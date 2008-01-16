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

                ISpellChecker spellChecker = SpellCheckManager.GetSpellChecker(_suggestion.Solution);

                if (spellChecker != null)
                {
                    foreach (string newWord in spellChecker.Suggest(_suggestion.Word, MAX_SUGGESTION_COUNT))
                    {
                        items.Add(new ReplaceWordWithBulbItem(_suggestion.Range, newWord));
                    }
                }                
                items.Add(new AddToDictionaryBulbItem(_suggestion.Word, _suggestion.Settings, _suggestion.Range));
                return items.ToArray();
            }
        }

        #endregion
    }
}