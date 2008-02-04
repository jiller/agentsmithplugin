using System;
using System.Collections.Generic;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith.NamingConventions
{
    [ConfigurableSeverityHighlighting(NAME)]
    public class NamingConventionsSuggestion : SuggestionBase
    {
        public const string NAME = "DelcarationDoesntConformToNamingConventions";
        private readonly IList<string> _exclusions;
        private readonly NamingConventionRule _rule;

        public NamingConventionsSuggestion(IDeclaration declaration, NamingConventionRule rule, IList<string> exclusions)
            : base(NAME, declaration, getDeclarationRange(declaration), rule.Description)
        {
            _rule = rule;
            _exclusions = exclusions;
        }

        public string[] NewNames
        {
            get { return _rule.GetCorrectedNames(Element as IDeclaration, _exclusions); }
        }

        private static DocumentRange getDeclarationRange(IDeclaration declaration)
        {
            if (declaration is INamespaceDeclaration)
            {
                return (((INamespaceDeclaration) declaration).GetDeclaredNameDocumentRange());
            }
            else
            {
                return declaration.GetNameDocumentRange();
            }
        }
    }
}