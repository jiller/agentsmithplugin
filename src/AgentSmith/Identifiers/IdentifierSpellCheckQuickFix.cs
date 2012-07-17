using System.Collections.Generic;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.Util;

namespace AgentSmith.Identifiers
{
    [QuickFix]
    public class IdentifierSpellCheckQuickFix : IQuickFix
    {
        private const uint MAX_SUGGESTION_COUNT = 5;

        private readonly IdentifierSpellCheckHighlighting _highlighting;

        public IdentifierSpellCheckQuickFix(IdentifierSpellCheckHighlighting highlighting)
        {
            this._highlighting = highlighting;
        }

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
                    foreach (string newWord in spellChecker.Suggest(this._highlighting.LexerToken.Value, MAX_SUGGESTION_COUNT))
                    {
                        if (newWord.IndexOf(" ") > 0)
                        {
                            continue;
                        }
                        string declaredName = this._highlighting.Declaration.DeclaredName;
                        string nameWithMisspelledWordDeleted = declaredName.Remove(this._highlighting.LexerToken.Start,
                            this._highlighting.LexerToken.Length);
                        string newName = nameWithMisspelledWordDeleted.Insert(this._highlighting.LexerToken.Start, newWord);

                        items.Add(new RenameBulbItem(this._highlighting.Declaration, newName));
                    }
                }
                items.Add(new RenameBulbItem(this._highlighting.Declaration));

                if (spellChecker != null)
                {
                    foreach (CustomDictionary dict in spellChecker.CustomDictionaries)
                    {
                        items.Add(new AddToDictionaryBulbItem(this._highlighting.MisspelledWord, dict.Name, this._highlighting.DocumentRange, _highlighting.SettingsStore ));
                    }
                }
                return items.ToArray();
            }
        }
    }
}