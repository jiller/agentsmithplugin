using System;
using AgentSmith.Options;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AgentSmith.Comments
{
    [ConfigurableSeverityHighlighting(NAME)]
    public class WordIsNotInDictionarySuggestion : SuggestionBase, IHighlighting
    {
        public const string NAME = "WordIsNotInDictionary";
        private readonly IClassMemberDeclaration _decl;

        private readonly DocumentRange _range;
        private readonly CommentsSettings _settings;
        private readonly ISolution _solution;
        private readonly string _word;

        public WordIsNotInDictionarySuggestion(string word, DocumentRange range, ISolution solution,
                                               CommentsSettings settings,
                                               IClassMemberDeclaration decl)
            : base(decl, String.Format("Word '{0}' is not in dictionary.", word))
        {
            _word = word;
            _range = range;
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

        public override Severity Severity
        {
            get { return HighlightingSettingsManager.Instance.Settings.GetSeverity(NAME); }
        }

        #endregion
    }
}