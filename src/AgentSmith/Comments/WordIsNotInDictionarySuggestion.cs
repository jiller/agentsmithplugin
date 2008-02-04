using System;
using AgentSmith.Options;
using AgentSmith.SpellCheck;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Editor;

namespace AgentSmith.Comments
{
    [ConfigurableSeverityHighlighting(NAME)]
    public class WordIsNotInDictionarySuggestion : SpellCheckSuggestionBase
    {
        public const string NAME = "WordIsNotInDictionary";

        public WordIsNotInDictionarySuggestion(string word, DocumentRange range, ISolution solution,
                                               CommentsSettings settings)
            : base(NAME, range, word, solution, settings)
        {
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