using System;
using System.Collections.Generic;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.ReSharper.Daemon;
using JetBrains.Util;

namespace AgentSmith.ResX
{
    [QuickFix]
    public class ResXQuickFix : IQuickFix
    {
        private const int MAX_SUGGESTIONS = 5;

        private readonly ResXSpellHighlighting _highlighting;

        public ResXQuickFix(ResXSpellHighlighting highlighting)
        {
            _highlighting = highlighting;
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
                ISpellChecker spellChecker = SpellCheckManager.GetSpellChecker(_highlighting.File, _highlighting.CustomDictionary.Name);
                foreach (string suggestion in spellChecker.Suggest(_highlighting.MisspelledWord, MAX_SUGGESTIONS))
                {
                    items.Add(new ReplaceWordWithBulbItem(_highlighting.Range, suggestion));
                }
                items.Add(new AddToDictionaryBulbItem(_highlighting.MisspelledWord,
                                                      _highlighting.CustomDictionary, _highlighting.Range));
                return items.ToArray();
            }
        }

        #endregion
    }
}