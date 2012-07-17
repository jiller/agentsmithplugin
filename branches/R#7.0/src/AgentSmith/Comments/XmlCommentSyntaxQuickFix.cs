using System;
using System.Collections.Generic;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.Util;

namespace AgentSmith.Comments
{
    [QuickFix]
    public class XmlCommentSyntaxQuickFix : IQuickFix
    {
        private const uint MAX_SUGGESTION_COUNT = 5;

        private readonly WordIsNotInDictionaryHighlight _highlight;

        public XmlCommentSyntaxQuickFix(WordIsNotInDictionaryHighlight highlight)
        {
            this._highlight = highlight;
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

                ISpellChecker spellChecker = this._highlight.SpellChecker;

                if (spellChecker != null)
                {
                    foreach (string suggestText in spellChecker.Suggest(this._highlight.MisspelledWord, MAX_SUGGESTION_COUNT))
                    {
                        string wordWithMisspelledWordDeleted = this._highlight.Word.Remove(this._highlight.Token.Start,
                           this._highlight.Token.Length);
                        string newWord = wordWithMisspelledWordDeleted.Insert(this._highlight.Token.Start, suggestText);
                        items.Add(new ReplaceWordWithBulbItem(this._highlight.DocumentRange, newWord));
                    }
                }

                items.Add(new ReplaceWordWithBulbItem(this._highlight.DocumentRange, String.Format("<c>{0}</c>", this._highlight.Word)));
                if (spellChecker != null)
                {
                    foreach (CustomDictionary customDict in spellChecker.CustomDictionaries)
                    {
                        items.Add(new AddToDictionaryBulbItem(this._highlight.Token.Value, customDict.Name, this._highlight.DocumentRange, _highlight.SettingsStore));
                    }
                }
                return items.ToArray();
            }
        }

        #endregion
    }
}