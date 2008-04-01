using System;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AgentSmith.Comments
{
    [ConfigurableSeverityHighlighting(NAME)]
    public class CanBeSurroundedWithMetatagsSuggestion : SuggestionBase
    {
        public const string NAME = "WordCanBeSurroundedWithMetaTags";

        private const string SUGGESTION_TEXT =
            "Word '{0}' appears to be an identifier and can be surrounded with metatag.";

        private readonly IClassMemberDeclaration _declaration;
        private readonly ISolution _solution;
        private readonly string _word;

        public CanBeSurroundedWithMetatagsSuggestion(
            string word, DocumentRange highlightingRange,
            IClassMemberDeclaration declaration, ISolution solution)
            : base(NAME, declaration, highlightingRange, String.Format(SUGGESTION_TEXT, word))
        {
            _solution = solution;
            _declaration = declaration;
            _word = word;
        }

        public IClassMemberDeclaration Declaration
        {
            get { return _declaration; }
        }

        public ISolution Solution
        {
            get { return _solution; }
        }

        public string Word
        {
            get { return _word; }
        }

        public static bool Enabled
        {
            get
            {
                return HighlightingSettingsManager.Instance.Settings.GetSeverity(NAME) !=
                       Severity.DO_NOT_SHOW;
            }
        }        
    }
}