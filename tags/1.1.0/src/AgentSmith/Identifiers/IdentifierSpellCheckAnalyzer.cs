using System;
using System.Collections.Generic;
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
        private readonly CommentsSettings _settings;
        private readonly ISpellChecker _spellChecker;

        private const int MAX_LENGTH_TO_SKIP = 3;

        public IdentifierSpellCheckAnalyzer(CommentsSettings settings, ISolution solution)
        {
            _solution = solution;
            _settings = settings;
            _spellChecker = SpellCheckManager.GetSpellChecker(solution);
        }

        public SuggestionBase[] Analyze(IDeclaration declaration)
        {
            if (!IdentifierSpellCheckSuggestion.Enabled || _spellChecker == null)
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

            HashSet<string> localNames = getLocalNames(declaration);

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
                        suggestions.Add(new IdentifierSpellCheckSuggestion(declaration, token, _solution, _settings));
                    }
                }
            }

            return suggestions.ToArray();
        }

        private HashSet<string> getLocalNames(IDeclaration declaration)
        {
            HashSet<string> localNames = new HashSet<string>();
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