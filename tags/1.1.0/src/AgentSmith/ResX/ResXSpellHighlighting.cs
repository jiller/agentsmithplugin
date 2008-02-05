using System;
using AgentSmith.Options;
using AgentSmith.SpellCheck;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Editor;

namespace AgentSmith.ResX
{
    [ConfigurableSeverityHighlighting(NAME)]
    public class ResXSpellHighlighting : SpellCheckSuggestionBase
    {
        public const string NAME = "ResxSpellCheckSuggestion";
        private readonly IProjectFile _file;

        public ResXSpellHighlighting(string word, IProjectFile file, CommentsSettings settings, DocumentRange range)
            : base(NAME, range, word, file.GetSolution(), settings)
        {
            _file = file;
        }

        public IProjectFile File
        {
            get { return _file; }
        }
    }
}