using System;
using System.Collections.Generic;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.ReSharper.Daemon;
using JetBrains.Util;

namespace AgentSmith.Strings
{
    [QuickFix]
    public class StringSpellCheckQuickFix : IQuickFix
    {
        private const uint MAX_SUGGESTION_COUNT = 5;

        private readonly StringSpellCheckSuggestion _suggestion;

        public StringSpellCheckQuickFix(StringSpellCheckSuggestion suggestion)
        {
            _suggestion = suggestion;
        }

        #region IQuickFix Members

        public bool IsAvailable(IUserDataHolder cache)
        {
            return true;
        }

        public IBulbItem[] Items
        {
            get
            {
                List<IBulbItem> items = new List<IBulbItem>();

                ISpellChecker spellChecker = _suggestion.SpellChecker;

                if (spellChecker != null)
                {
                    foreach (string newWord in spellChecker.Suggest(_suggestion.MisspelledWord, MAX_SUGGESTION_COUNT))
                    {
                        string wordWithMisspelledWordDeleted =
                            _suggestion.Word.Remove(_suggestion.MisspelledRange.StartOffset,
                                                    _suggestion.MisspelledRange.Length);

                        string newString = wordWithMisspelledWordDeleted.Insert(
                            _suggestion.MisspelledRange.StartOffset, newWord);

                        items.Add(new ReplaceWordWithBulbItem(_suggestion.Range, newString));
                    }

                    foreach (CustomDictionary dict in spellChecker.CustomDictionaries)
                    {
                        items.Add(new AddToDictionaryBulbItem(_suggestion.MisspelledWord, dict, _suggestion.Range));
                    }
                }
                return items.ToArray();
            }
        }

        #endregion
    }
}