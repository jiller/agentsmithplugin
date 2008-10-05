using System;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.Util;

namespace AgentSmith.Strings
{    
    [ConfigurableSeverityHighlighting(NAME)]
    public class VerbatimStringSpellCheckSuggestion : StringSpellCheckSuggestionBase
    {
        public const string NAME = "VerbatimStringLiteralsWordIsNotInDictionary";

        public VerbatimStringSpellCheckSuggestion(string word, DocumentRange range, 
            string misspelledWord, TextRange misspelledRange,
            ISolution solution, ISpellChecker spellChecker) :
            base(NAME, word, range, misspelledWord, misspelledRange, solution, spellChecker)
        {
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