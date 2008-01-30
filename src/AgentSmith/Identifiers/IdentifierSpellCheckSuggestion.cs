using System;
using AgentSmith.Options;
using AgentSmith.SpellCheck;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith.Identifiers
{
    [ConfigurableSeverityHighlighting(NAME)]
    public class IdentifierSpellCheckSuggestion : SpellCheckSuggestionBase
    {
        public const string NAME = "IdentifierWordIsNotInDictionary";

        private readonly LexerToken _lexerToken;
        private readonly IDeclaration _declaration;

        public IdentifierSpellCheckSuggestion(IDeclaration declaration,
                                              LexerToken token,
                                              ISolution solution, CommentsSettings settings)
            : base(declaration is INamespaceDeclaration ? ((INamespaceDeclaration)declaration).GetDeclaredNameDocumentRange() : 
                        declaration.GetNameDocumentRange(), token.Value, solution, settings)
        {
            _lexerToken = token;
            _declaration = declaration;
        }

        public IDeclaration Declaration
        {
            get { return _declaration; }
        }

        public LexerToken LexerToken
        {
            get { return _lexerToken; }
        }

        public override Severity Severity
        {
            get { return HighlightingSettingsManager.Instance.Settings.GetSeverity(NAME); }
        }

        public override string AttributeId
        {
            get { return HighlightingAttributeIds.GetDefaultAttribute(Severity.SUGGESTION); }
        }

        public static bool Enabled
        {
            get
            {
                return HighlightingSettingsManager.Instance.Settings.GetSeverity(NAME) !=
                       Severity.DO_NOT_SHOW;
            }
        }
    }
}