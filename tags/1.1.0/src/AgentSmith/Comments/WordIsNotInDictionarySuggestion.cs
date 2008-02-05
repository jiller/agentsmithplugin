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
        private readonly string _word;
        private readonly LexerToken _token;

        public WordIsNotInDictionarySuggestion(string word, DocumentRange range,
                                               LexerToken misspelledToken, ISolution solution, CommentsSettings settings)
            : base(NAME, range, misspelledToken.Value, solution, settings)
        {
            _word = word;
            _token = misspelledToken;
        }

        public string Word
        {
            get { return _word; }
        }

        public LexerToken Token
        {
            get { return _token; }
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