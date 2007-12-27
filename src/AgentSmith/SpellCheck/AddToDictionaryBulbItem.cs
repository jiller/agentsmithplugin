using System;
using AgentSmith.Options;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.TextControl;

namespace AgentSmith.SpellCheck
{
    public class AddToDictionaryBulbItem : IBulbItem
    {
        private readonly string _word;
        private readonly CommentsSettings _settings;
        private DocumentRange _documentRange;

        public AddToDictionaryBulbItem(string word, CommentsSettings settings, DocumentRange range)
        {
            _word = word;
            _settings = settings;
            _documentRange = range;
        }

        #region IBulbItem Members

        public void Execute(ISolution solution, ITextControl textControl)
        {
            string words = _settings.UserWords.Trim();
            if (words.Length > 0)
            {
                _settings.UserWords = words + "\n";
            }
            _settings.UserWords += _word;

            Daemon.GetInstance(solution).ForceReHighlight(_documentRange.Document);
        }

        public string Text
        {
            get { return String.Format("Add '{0}' to the dictionary", _word); }
        }

        #endregion
    }
}