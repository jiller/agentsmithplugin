using System;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.TextControl;

namespace AgentSmith.SpellCheck
{
    /// <summary>
    /// Bulb item that adds word not recognized by spell checker to custom dictionary.
    /// </summary>
    public class AddToDictionaryBulbItem : IBulbItem
    {
        private readonly string _word;
        private readonly CustomDictionary _customDictionary;
        private DocumentRange _documentRange;

        /// <summary>
        /// Initializes new instance.
        /// </summary>
        /// <param name="word">Unrecognized word.</param>
        /// <param name="settings">Custom dictionary instance.</param>
        /// <param name="range">Range in document which misspelled work occupies.</param>
        public AddToDictionaryBulbItem(string word, CustomDictionary settings, DocumentRange range)
        {
            _word = word;
            _customDictionary = settings;
            _documentRange = range;
        }

        #region IBulbItem Members

        public void Execute(ISolution solution, ITextControl textControl)
        {
            string words = _customDictionary.UserWords.Trim();
            if (words.Length > 0)
            {
                _customDictionary.UserWords = words + "\n";
            }
            _customDictionary.UserWords += _word;

            Daemon.GetInstance(solution).ForceReHighlight(_documentRange.Document);
        }

        public string Text
        {
            get { return String.Format("Add '{0}' to '{1}' user dictionary", _word, _customDictionary.Name); }
        }

        #endregion
    }
}