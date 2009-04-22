using System;
using System.Collections.Generic;
using AgentSmith.NamingConventions;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Intentions;
using JetBrains.Util;

namespace AgentSmith.Identifiers
{
    [QuickFix]
    public class IdentifierSpellCheckQuickFix : IQuickFix
    {
        private const uint MAX_SUGGESTION_COUNT = 5;

        private readonly IdentifierSpellCheckSuggestion _suggestion;

        public IdentifierSpellCheckQuickFix(IdentifierSpellCheckSuggestion suggestion)
        {
            _suggestion = suggestion;
        }

        public bool IsAvailable(IUserDataHolder cache)
        {
            return _suggestion.Declaration.IsValid();
        }

        public IBulbItem[] Items
        {
            get
            {
                List<IBulbItem> items = new List<IBulbItem>();
                
                ISpellChecker spellChecker = _suggestion.SpellChecker;

                if (spellChecker != null)
                {
                    foreach (string newWord in spellChecker.Suggest(_suggestion.LexerToken.Value, MAX_SUGGESTION_COUNT))
                    {
                        if (newWord.IndexOf(" ") > 0)
                        {
                            continue;
                        }
                        string declaredName = _suggestion.Declaration.DeclaredName;
                        string nameWithMisspelledWordDeleted = declaredName.Remove(_suggestion.LexerToken.Start,
                            _suggestion.LexerToken.Length);
                        string newName = nameWithMisspelledWordDeleted.Insert(_suggestion.LexerToken.Start, newWord);

                        items.Add(new NamingConventionsBulbItem(_suggestion.Declaration, newName));
                    }
                }
                items.Add(new RenameBulbItem(_suggestion.Declaration));

                if (spellChecker != null)
                {
                    foreach (CustomDictionary dict in spellChecker.CustomDictionaries)
                    {
                        items.Add(new AddToDictionaryBulbItem(_suggestion.MisspelledWord, dict, _suggestion.Range));
                    }
                }
                return items.ToArray();
            }
        }
    }
}