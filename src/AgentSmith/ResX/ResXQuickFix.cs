using System;
using System.Collections.Generic;
using AgentSmith.SpellCheck;
using JetBrains.ReSharper.Daemon;
using JetBrains.Util;

namespace AgentSmith.Resx
{
    [QuickFix]
    public class ResXQuickFix : IQuickFix
    {
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
                foreach (string suggestion in SpellCheckManager.GetSpellChecker(_highlighting.File).Suggest(_highlighting.Word, 5))
                {
                    items.Add(new ReplaceWordWithBulbItem(_highlighting.Range, suggestion));
                }
                items.Add(new AddToDictionaryBulbItem(_highlighting.Word, _highlighting.Settings, _highlighting.Range));
                return items.ToArray();
            }
        }

        #endregion
    }
}