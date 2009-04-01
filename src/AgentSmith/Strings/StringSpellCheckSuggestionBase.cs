using System;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.Util;

namespace AgentSmith.Strings
{
    public abstract class StringSpellCheckSuggestionBase : SpellCheckSuggestionBase
    {
        private readonly string _word;        
        private readonly TextRange _misspelledRange;
        private readonly char _ampersandChar = (char)0;        

        public StringSpellCheckSuggestionBase(string name, string word, DocumentRange range, string misspelledWord,
                                          TextRange misspelledRange, ISolution solution,
                                          ISpellChecker spellChecker)
            : base(name, range, misspelledWord.Replace("&", ""), solution, spellChecker)
        {
            _word = word;            
            _misspelledRange = misspelledRange;
            int ampersandIndex = misspelledWord.IndexOf("&");
            
            if (ampersandIndex >=0 && ampersandIndex < misspelledWord.Length - 1)
            {
                _ampersandChar = misspelledWord[ampersandIndex + 1];
            }                       
        }

        public string Word
        {
            get { return _word; }
        }

        public TextRange MisspelledRange
        {
            get { return _misspelledRange; }
        }
       
        public char AmpersandChar
        {
            get { return _ampersandChar; }
        }

        public override Severity Severity
        {
            get
            {
                Severity severity = HighlightingSettingsManager.Instance.Settings.GetSeverity(SuggestionName);
                return severity == Severity.DO_NOT_SHOW ? severity : Severity.INFO;
            }
        }

        public override string AttributeId
        {
            get { return HighlightingAttributeIds.SUGGESTION_ATTRIBUTE; }
        }
    }
}
