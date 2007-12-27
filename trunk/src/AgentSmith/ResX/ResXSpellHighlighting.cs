using System;
using AgentSmith.Options;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith.Resx
{
    [ConfigurableSeverityHighlighting(NAME)]
    public class ResXSpellHighlighting : SuggestionBase
    {
        public const string NAME = "ResxSpellCheckSuggestion";
        private readonly DocumentRange _range;
        private readonly CommentsSettings _settings;
        private readonly string _word;
        private readonly IProjectFile _file;

        public ResXSpellHighlighting(string word, IProjectFile file, CommentsSettings settings, DocumentRange range, IElement element, string toolTip)
            : base(element, toolTip)
        {
            _range = range;
            _settings = settings;
            _word = word;
            _file = file;
        }

        public CommentsSettings Settings
        {
            get { return _settings; }
        }

        public string Word
        {
            get { return _word; }
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