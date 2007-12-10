using System;
using System.Drawing;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp.Stages;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith.Highlights
{
    public abstract class SuggestionBase : CSharpHighlightingBase, IHighlighting
    {
        private readonly DocumentRange _range;
        private readonly string _tooltip;
        private readonly IElement _element;

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

        public virtual string AttributeId
        {
            get { return ""; }
        }

        public abstract Color ColorOnStripe { get; }

        public bool ShowToolTipInStatusBar
        {
            get { return false; }
        }

        public int NavigationOffset
        {
            get { return 0; }
        }

        public OverlapResolvePolicy OverlapResolvePolicy
        {
            get { return OverlapResolvePolicy.WARNING; }
        }

        public override DocumentRange Range
        {
            get { return _range; }
        }

        public abstract Severity Severity { get; }

        public string ToolTip
        {
            get { return _tooltip; }
        }

        public string ErrorStripeToolTip
        {
            get { return _tooltip; }
        }

        public int NavigationOffsetPatch
        {
            get { return 0; }
        }
    }
}