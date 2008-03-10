using System;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith.Identifiers
{
    [ConfigurableSeverityHighlighting(NAME)]
    public class IdentifierSpellCheckSuggestion : SpellCheckSuggestionBase
    {
        public const string NAME = "IdentifierWordIsNotInDictionary";

        private readonly IDeclaration _declaration;
        private readonly LexerToken _lexerToken;

        public IdentifierSpellCheckSuggestion(IDeclaration declaration, LexerToken token,
                                              ISolution solution, CustomDictionary customDictionary)
            : base(NAME, getRange(declaration), token.Value, solution, customDictionary)
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
            get
            {
                Severity severity = HighlightingSettingsManager.Instance.Settings.GetSeverity(NAME);
                return severity == JetBrains.ReSharper.Daemon.Severity.DO_NOT_SHOW ? severity : Severity.INFO;
            }
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

        private static DocumentRange getRange(IDeclaration declaration)
        {
            if (declaration is INamespaceDeclaration)
            {
                return ((INamespaceDeclaration) declaration).GetDeclaredNameDocumentRange();
            }
            else
            {
                return declaration.GetNameDocumentRange();
            }
        }
    }
}