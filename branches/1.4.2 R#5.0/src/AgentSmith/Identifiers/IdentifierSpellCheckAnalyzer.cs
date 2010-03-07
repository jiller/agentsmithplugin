using System;
using System.Collections.Generic;
using AgentSmith.MemberMatch;
using AgentSmith.Options;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentSmith.Identifiers
{
    public class IdentifierSpellCheckAnalyzer : IDeclarationAnalyzer
    {
        private readonly ISolution _solution;        
        private readonly ISpellChecker _spellChecker;
        private readonly CodeStyleSettings _settings;

        private const int MAX_LENGTH_TO_SKIP = 3;

        public IdentifierSpellCheckAnalyzer(string dictionaryName, ISolution solution, CodeStyleSettings settings)
        {
            _solution = solution;            
            _spellChecker = SpellCheckManager.GetSpellChecker(solution, dictionaryName);
            _settings = settings;

            ComplexMatchEvaluator.Prepare(solution, settings.IdentifiersToSpellCheck,
                settings.IdentifiersNotToSpellCheck);
        }

        public SuggestionBase[] Analyze(IDeclaration declaration, bool spellCheck)
        {
            if (!IdentifierSpellCheckSuggestion.Enabled || _spellChecker == null || !spellCheck)
            {
                return null;
            }

            if (declaration is IIndexerDeclaration ||
                declaration is IDestructorDeclaration ||
                declaration is IAccessorDeclaration ||
                declaration is IConstructorDeclaration ||
                (declaration.DeclaredName.Contains(".") && !(declaration is INamespaceDeclaration)))
            {
                return null;
            }

            if (ComplexMatchEvaluator.IsMatch(declaration, _settings.IdentifiersToSpellCheck,
                    _settings.IdentifiersNotToSpellCheck, true) == null)
            {
                return null;
            }

            OrderedHashSet<string> localNames = getLocalNames(declaration);

            CamelHumpLexer lexer =
                new CamelHumpLexer(declaration.DeclaredName, 0, declaration.DeclaredName.Length);

            List<SuggestionBase> suggestions = new List<SuggestionBase>();
            foreach (LexerToken token in lexer)
            {
                string val = token.Value;
                string lowerVal = val.ToLower();
                if (val.Length > MAX_LENGTH_TO_SKIP &&
                    SpellCheckUtil.ShouldSpellCheck(val) &&
                    !localNames.Contains(lowerVal) &&
                    !_spellChecker.TestWord(val, false))
                {
                    bool found = false;
                    foreach (string entry in localNames)
                    {
                        if (entry.StartsWith(lowerVal))
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        suggestions.Add(new IdentifierSpellCheckSuggestion(declaration, token, _solution, _spellChecker));
                    }
                }
            }

            return suggestions.ToArray();
        }

        private OrderedHashSet<string> getLocalNames(IDeclaration declaration)
        {
            OrderedHashSet<string> localNames = new OrderedHashSet<string>();
            ITypeOwner var = declaration as ITypeOwner;
            if (var != null)
            {
                string name = var.Type.GetPresentableName(declaration.Language);
                string acronym = "";
                foreach (char c in name)
                {
                    if (char.IsUpper(c))
                    {
                        acronym += c;
                    }
                }
                localNames.Add(acronym.ToLower());

                CamelHumpLexer lexer = new CamelHumpLexer(name, 0, name.Length);
                foreach (LexerToken token in lexer)
                {
                    localNames.Add(token.Value.ToLower());
                }
            }

            IClassLikeDeclaration decl = declaration as IClassLikeDeclaration;
            if (decl != null)
            {
                foreach (IDeclaredType type in decl.SuperTypes)
                {
                    string name = type.GetPresentableName(declaration.Language);
                    CamelHumpLexer lexer = new CamelHumpLexer(name, 0, name.Length);
                    foreach (LexerToken token in lexer)
                    {
                        localNames.Add(token.Value.ToLower());
                    }
                }
            }
            return localNames;
        }
    }
}