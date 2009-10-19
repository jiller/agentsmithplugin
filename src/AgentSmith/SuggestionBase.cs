using System;
using System.Drawing;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith
{
    /// <summary>
    /// Base suggestion for Agent Smith suggestionsf. 
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
            get
            {
                switch (Severity)
                {
                    case Severity.ERROR:
                        return HighlightingAttributeIds.ERROR_ATTRIBUTE;
                    case Severity.WARNING:
                        return HighlightingAttributeIds.WARNING_ATTRIBUTE;
                    case Severity.SUGGESTION:
                        return HighlightingAttributeIds.SUGGESTION_ATTRIBUTE;
                    case Severity.HINT:
                        return HighlightingAttributeIds.HINT_ATTRIBUTE;
                    case Severity.INFO:
                    case Severity.DO_NOT_SHOW:
                        return null;
                }
                return null;
            }
        }

        public virtual Color ColorOnStripe
        {
            get { return Color.Empty; }
        }

        public bool ShowToolTipInStatusBar
        {
            get { return true; }
        }

        public OverlapResolveKind OverlapResolvePolicy
        {
            get { return OverlapResolveKind.WARNING; }
        }

        public virtual Severity Severity
        {
            get { return HighlightingSettingsManager.Instance.Settings.GetSeverity(_suggestionName); }
        }
        
        public string SuggestionName
        {
            get { return _suggestionName; }
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

        public bool IsValid()
        {
            return _range.IsValid() && (_element == null ||_element.IsValid());
        }

        #endregion
    }
}