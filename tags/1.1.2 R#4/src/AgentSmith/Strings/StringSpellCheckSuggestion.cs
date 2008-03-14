using System;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.Util;

namespace AgentSmith.Strings
{    
    [ConfigurableSeverityHighlighting(NAME)]
    public class StringSpellCheckSuggestion : SpellCheckSuggestionBase
    {
        public const string NAME = "StringLiteralsWordIsNotInDictionary";

        private readonly string _word;        
        private readonly TextRange _misspelledRange;

        public StringSpellCheckSuggestion(string word, DocumentRange range, string misspelledWord,
                                          TextRange misspelledRange, ISolution solution,
                                          CustomDictionary customDictionary)
            : base(NAME, range, misspelledWord, solution, customDictionary)
        {
            _word = word;            
            _misspelledRange = misspelledRange;
        }

        public string Word
        {
            get { return _word; }
        }

        public TextRange MisspelledRange
        {
            get { return _misspelledRange; }
        }
        
        public override Severity Severity
        {
            get
            {
                Severity severity = HighlightingSettingsManager.Instance.Settings.GetSeverity(NAME);
                return severity == JetBrains.ReSharper.Daemon.Severity.DO_NOT_SHOW ? severity : Severity.INFO;
            }
        }

        public override string AttributeId
        {
            get { return HighlightingAttributeIds.SUGGESTION_ATTRIBUTE; }
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