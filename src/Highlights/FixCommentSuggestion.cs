using System;
using System.Drawing;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith.Highlights
{
    [ConfigurableSeverityHighlighting(Name)]
    internal class FixCommentSuggestion : SuggestionBase
    {
        public const string Name = "PublicMembersMustHaveComments";

        public FixCommentSuggestion(IDeclaration element, string toolTip)
            : base(element, toolTip)
        {
        }

        public override string AttributeId
        {
            get { return HighlightingAttributeIds.GetDefaultAttribute(Severity); }
        }

        public override Severity Severity
        {
            get { return HighlightingSettingsManager.Instance.Settings.GetSeverity(Name); }
        }


        public override Color ColorOnStripe
        {
            get { return Color.Empty; }
        }
    }
}