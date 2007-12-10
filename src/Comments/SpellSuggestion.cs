using System;
using System.Drawing;
using AgentSmith.Options;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp.Stages;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using NetSpell.SpellChecker.Dictionary;

namespace AgentSmith.Comments
{
    [ConfigurableSeverityHighlighting(SpellSuggestion.Name)]
    public class SpellSuggestion : CSharpHighlightingBase, IHighlighting
    {
        public const string Name = "WordIsNotInDictionary";
        
        private readonly WordDictionary _dictionary;
        private readonly DocumentRange _range;
        private readonly CommentsSettings _settings;
        private readonly ISolution _solution;
        private readonly string _word;
        private readonly IClassMemberDeclaration _decl;

        public SpellSuggestion(string word, DocumentRange range, ISolution solution,
                               CommentsSettings settings, WordDictionary dictionary, IClassMemberDeclaration decl)
        {
            _word = word;
            _range = range;
            _dictionary = dictionary;
            _solution = solution;
            _settings = settings;
            _decl = decl;
        }

        public override DocumentRange Range
        {
            get { return _range; }
        }

        public IClassMemberDeclaration Declaration
        {
            get { return _decl; }
        }

        public WordDictionary Dictionary
        {
            get { return _dictionary; }
        }

        public ISolution Solution
        {
            get { return _solution; }
        }

        public CommentsSettings Settings
        {
            get { return _settings; }
        }

        public string Word
        {
            get { return _word; }
        }

        #region IHighlighting Members

        public string AttributeId
        {
            get { return HighlightingAttributeIds.GetDefaultAttribute(Severity); }
        }

        public OverlapResolvePolicy OverlapResolvePolicy
        {
            get { return OverlapResolvePolicy.UNRESOLVED_ERROR; }
        }

        public Severity Severity
        {
            get { return HighlightingSettingsManager.Instance.Settings.GetSeverity(Name); }
        }

        public string ToolTip
        {
            get { return String.Format("Word '{0}' is not in dictionary.", _word); }
        }

        public string ErrorStripeToolTip
        {
            get { return ToolTip; }
        }

        public Color ColorOnStripe
        {
            get { return Color.Empty; }
        }

        public bool ShowToolTipInStatusBar
        {
            get { return true; }
        }

        public int NavigationOffsetPatch
        {
            get { return 0; }
        }

        #endregion
    }
}