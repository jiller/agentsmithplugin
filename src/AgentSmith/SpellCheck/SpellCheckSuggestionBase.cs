using System;
using AgentSmith.Options;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Editor;

namespace AgentSmith.SpellCheck
{
    public abstract class SpellCheckSuggestionBase : SuggestionBase
    {
        private readonly string _word;
        private readonly ISolution _solution;
        private readonly CommentsSettings _settings;

        public SpellCheckSuggestionBase(string suggestionName, DocumentRange range, string word,
            ISolution solution, CommentsSettings settings)
            : base(suggestionName, null, range, String.Format("Word '{0}' is not in dictionary.", word))
        {
            _word = word;
            _solution = solution;
            _settings = settings;
        }

        public string Word
        {
            get { return _word; }
        }

        public ISolution Solution
        {
            get { return _solution; }
        }

        public CommentsSettings Settings
        {
            get { return _settings; }
        }
    }
}