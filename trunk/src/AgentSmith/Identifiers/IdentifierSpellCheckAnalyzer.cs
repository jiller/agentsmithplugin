using System;
using System.Collections.Generic;
using AgentSmith.Options;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith.Identifiers
{
    public class IdentifierSpellCheckAnalyzer : IDeclarationAnalyzer
    {
        private readonly ISolution _solution;
        private readonly CommentsSettings _settings;
        private readonly ISpellChecker _spellChecker;

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
                (declaration.DeclaredName.Contains(".") && !(declaration is INamespaceDeclaration)))
            {
                return null;
            }

            if (isAcronym(declaration))
            {
                return null;
            }

            CamelHumpLexer lexer =
                new CamelHumpLexer(declaration.DeclaredName, 0, declaration.DeclaredName.Length);

            List<SuggestionBase> suggestions = new List<SuggestionBase>();
            foreach (LexerToken token in lexer)
            {
                if (SpellCheckUtil.ShouldSpellCheck(token.Value) &&
                    !_spellChecker.TestWord(token.Value, false))
                {
                    suggestions.Add(new IdentifierSpellCheckSuggestion(declaration, token, _solution, _settings));
                }
            }

            return suggestions.ToArray();
        }

        private bool isAcronym(IDeclaration declaration)
        {
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
                return acronym == declaration.DeclaredName;
            }
            return false;
        }
    }
}