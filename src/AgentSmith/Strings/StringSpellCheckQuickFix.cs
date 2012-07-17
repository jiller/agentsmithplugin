using System;
using System.Collections.Generic;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.Util;

namespace AgentSmith.Strings
{
    [QuickFix]
    public class StringSpellCheckQuickFix : IQuickFix
    {
        private const uint MAX_SUGGESTION_COUNT = 5;

        private readonly StringSpellCheckHighlighting _highlighting;

        public StringSpellCheckQuickFix(StringSpellCheckHighlighting highlighting)
        {
            this._highlighting = highlighting;
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

                ISpellChecker spellChecker = this._highlighting.SpellChecker;
                
                if (spellChecker != null)
                {
                    foreach (string newWord in spellChecker.Suggest(this._highlighting.MisspelledWord, MAX_SUGGESTION_COUNT))
                    {
                        string wordWithMisspelledWordDeleted =
                            this._highlighting.Word.Remove(this._highlighting.MisspelledRange.StartOffset,
                            this._highlighting.MisspelledRange.Length);

                        string newString = wordWithMisspelledWordDeleted.Insert(
                            this._highlighting.MisspelledRange.StartOffset, newWord);

                        items.Add(new ReplaceWordWithBulbItem(this._highlighting.DocumentRange, newString));
                    }

                    foreach (CustomDictionary dict in spellChecker.CustomDictionaries)
                    {
                        items.Add(new AddToDictionaryBulbItem(this._highlighting.MisspelledWord, dict.Name, this._highlighting.DocumentRange, _highlighting.SettingsStore));
                    }
                }
                return items.ToArray();
            }
        }

        #endregion
    }
}