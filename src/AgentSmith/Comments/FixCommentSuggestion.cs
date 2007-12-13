using System;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith.Comments
{
    [ConfigurableSeverityHighlighting(NAME)]
    public class FixCommentSuggestion : SuggestionBase
    {
        public const string NAME = "PublicMembersMustHaveComments";

        public FixCommentSuggestion(IDeclaration element, string toolTip)
            : base(element, toolTip)
        {
        }

        public override Severity Severity
        {
            get { return HighlightingSettingsManager.Instance.Settings.GetSeverity(NAME); }
        }
    }
}