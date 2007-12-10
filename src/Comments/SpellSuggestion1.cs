using System;
using System.Drawing;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp.Stages;
using JetBrains.ReSharper.Editor;

namespace AgentSmith.Comments
{
    [ConfigurableSeverityHighlighting("Hello")]
    public class SpellSuggestion1 : CSharpHighlightingBase, IHighlighting
    {
        private string _toolTip;
        private DocumentRange _range;

        public SpellSuggestion1(string toolTip, DocumentRange range)
        {
            _toolTip = toolTip;
            _range = range;
        }

        public override DocumentRange Range
        {
            get { return _range; }
        }

        #region IHighlighting Members

        string IHighlighting.AttributeId
        {
            get { return Highlighting.SUGGESTION_ID; }
        }

        OverlapResolvePolicy IHighlighting.OverlapResolvePolicy
        {
            get { return OverlapResolvePolicy.UNRESOLVED_ERROR; }
        }

        Severity IHighlighting.Severity
        {
            get { return Severity.WARNING; }
        }

        string IHighlighting.ToolTip
        {
            get { return _toolTip; }
        }

        string IHighlighting.ErrorStripeToolTip
        {
            get { return _toolTip; }
        }

        Color IHighlighting.ColorOnStripe
        {
            get { return Color.Empty; }
        }

        bool IHighlighting.ShowToolTipInStatusBar
        {
            get { return true; }
        }

        int IHighlighting.NavigationOffsetPatch
        {
            get { return 0; }
        }

        #endregion

    }
}