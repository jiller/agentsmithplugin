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
        private readonly string _toolTip;
        private readonly string _suggestionName;

        public SuggestionBase(string suggestionName, IElement element, DocumentRange highlightingRange, string toolTip)
        {
            _range = highlightingRange;
            _toolTip = toolTip;
            _element = element;
            _suggestionName = suggestionName;
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

        public virtual Severity Severity
        {
            get { return HighlightingSettingsManager.Instance.Settings.GetSeverity(_suggestionName); }
        }
        
        public virtual string ToolTip
        {
            get { return _toolTip + "[Agent Smith]"; }
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