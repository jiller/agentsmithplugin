using System;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.TextControl;

namespace AgentSmith.SpellCheck
{
    public class AddToDictionaryBulbItem : IBulbItem
    {
        private readonly string _word;
        private readonly CustomDictionary _customDictionary;
        private DocumentRange _documentRange;

        public AddToDictionaryBulbItem(string word, CustomDictionary settings, DocumentRange range)
        {
            _word = word;
            _customDictionary = settings;
            _documentRange = range;
        }

        #region IBulbItem Members

        public void Execute(ISolution solution, ITextControl textControl)
        {
            string words = _customDictionary.DecodedUserWords.Trim();
            if (words.Length > 0)
            {
                _customDictionary.DecodedUserWords = words + "\n";
            }
            _customDictionary.DecodedUserWords += _word;

            Daemon.GetInstance(solution).ForceReHighlight(_documentRange.Document);
        }

        public string Text
        {
            get { return String.Format("Add '{0}' to '{1}' user dictionary", _word, _customDictionary.Name); }
        }

        #endregion
    }
}