using System;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AgentSmith.Comments
{
    [ConfigurableSeverityHighlighting(NAME)]
    public class CanBeSurroundedWithMetatagsSuggestion : SuggestionBase, IHighlighting
    {
        public const string NAME = "WordCanBeSurroundedWithMetaTags";
        private readonly DocumentRange _range;
        private readonly IClassMemberDeclaration _decl;
        private readonly ISolution _solution;
        private readonly string _word;

        public CanBeSurroundedWithMetatagsSuggestion(string word, DocumentRange range, IClassMemberDeclaration decl, ISolution solution)
            :base(decl,
                String.Format("Word '{0}' appears to be an identifier and can be surrounded with metatag.", word))
        {
            _range = range;
            _solution = solution;
            _decl = decl;
            _word = word;
        }

        public override DocumentRange Range
        {
            get { return _range; }
        }

        public IClassMemberDeclaration Declaration
        {
            get { return _decl; }
        }

        public ISolution Solution
        {
            get { return _solution; }
        }

        public string Word
        {
            get { return _word; }
        }

        #region IHighlighting Members

        public override Severity Severity
        {
            get { return HighlightingSettingsManager.Instance.Settings.GetSeverity(NAME); }
        }

        public static bool Enabled
        {
            get
            {
                return HighlightingSettingsManager.Instance.Settings.GetSeverity(NAME) !=
                       Severity.DO_NOT_SHOW;
            }
        }

        #endregion
    }
}