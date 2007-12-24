using System;
using System.Drawing;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith
{
    /// <summary>
    /// Base suggestion for Agent Smith suggestions.
    /// </summary>
    public abstract class SuggestionBase : IHighlighting
    {
        private readonly IElement _element;
        private readonly DocumentRange _range;
        private readonly string _tooltip;

        public SuggestionBase(IElement element, string toolTip)
        {
            _range = element.GetDocumentRange();
            _tooltip = toolTip;
            _element = element;
        }

        public SuggestionBase(IDeclaration element, string toolTip)
        {
            _range = element.GetNameDocumentRange();
            _tooltip = toolTip;
            _element = element;
        }

        public SuggestionBase(DocumentRange range, string tooltip)
        {
            _range = range;
            _tooltip = tooltip;
        }

        public IElement Element
        {
            get { return _element; }
        }

        public int NavigationOffset
        {
            get { return 0; }
        }

        public virtual DocumentRange Range
        {
            get { return _range; }
        }

        #region IHighlighting Members

        public virtual string AttributeId
        {
            get { return HighlightingAttributeIds.GetDefaultAttribute(Severity); }
        }

        public virtual Color ColorOnStripe
        {
            get { return Color.Empty; }
        }

        public bool ShowToolTipInStatusBar
        {
            get { return true; }
        }

        public OverlapResolvePolicy OverlapResolvePolicy
        {
            get { return OverlapResolvePolicy.WARNING; }
        }

        public abstract Severity Severity { get; }

        public virtual string ToolTip
        {
            get { return _tooltip + "[Agent Smith]"; }
        }

        public string ErrorStripeToolTip
        {
            get { return ToolTip; }
        }

        public int NavigationOffsetPatch
        {
            get { return 0; }
        }

        #endregion
    }
}