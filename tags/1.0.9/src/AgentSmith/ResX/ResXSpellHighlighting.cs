using System;
using AgentSmith.Options;
using AgentSmith.SpellCheck;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith.Resx
{
    [ConfigurableSeverityHighlighting(NAME)]
    public class ResXSpellHighlighting : SpellCheckSuggestionBase
    {
        public const string NAME = "ResxSpellCheckSuggestion";
        private readonly DocumentRange _range;
        private readonly IProjectFile _file;

        public ResXSpellHighlighting(string word, IProjectFile file, CommentsSettings settings, DocumentRange range, IElement element)
            : base(element, word, file.GetSolution(), settings)
        {
            _range = range;                    
            _file = file;
        }

        public override Severity Severity
        {
            get { return HighlightingSettingsManager.Instance.Settings.GetSeverity(NAME); }
        }

        public override DocumentRange Range
        {
            get { return _range; }
        }

        public IProjectFile File
        {
            get { return _file; }
        }
    }
}