using System;
using AgentSmith.Options;
using AgentSmith.SpellCheck;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AgentSmith.Comments
{
    [ConfigurableSeverityHighlighting(NAME)]
    public class WordIsNotInDictionarySuggestion : SpellCheckSuggestionBase, IHighlighting
    {
        public const string NAME = "WordIsNotInDictionary";

        private readonly IClassMemberDeclaration _decl;
        private readonly DocumentRange _range;


        public WordIsNotInDictionarySuggestion(string word, DocumentRange range, ISolution solution,
                                               CommentsSettings settings,
                                               IClassMemberDeclaration decl)
            : base(decl, word, solution, settings)
        {
            _range = range;
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

        public static bool Enabled
        {
            get
            {
                return HighlightingSettingsManager.Instance.Settings.GetSeverity(NAME) !=
                       Severity.DO_NOT_SHOW;
            }
        }

        #region IHighlighting Members

        public override Severity Severity
        {
            get { return HighlightingSettingsManager.Instance.Settings.GetSeverity(NAME); }
        }

        #endregion
    }
}