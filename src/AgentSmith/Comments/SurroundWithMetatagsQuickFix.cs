using System;
using System.Collections.Generic;
using AgentSmith.SpellCheck;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Intentions;
using JetBrains.Util;

namespace AgentSmith.Comments
{
    [QuickFix]
    public class SurroundWithMetatagsQuickFix: IQuickFix
    {
        private readonly CanBeSurroundedWithMetatagsSuggestion _suggestion;
        public SurroundWithMetatagsQuickFix(CanBeSurroundedWithMetatagsSuggestion suggestion)
        {
            _suggestion = suggestion;
        }

        public bool IsAvailable(IUserDataHolder cache)
        {
            return _suggestion.Range.IsValid();
        }

        public IBulbItem[] Items
        {
            get
            {
                List<IBulbItem> items = new List<IBulbItem>();

                IList<string> replaceFormats =
                    IdentifierResolver.GetReplaceFormats(_suggestion.Declaration,
                    _suggestion.Solution, _suggestion.Word);

                foreach (string format in replaceFormats)
                {
                    string replacement = String.Format(format, _suggestion.Word);
                    items.Add(new ReplaceWordWithBulbItem(_suggestion.Range, replacement));
                }

                items.Add(new ReplaceWordWithBulbItem(_suggestion.Range, String.Format("<c>{0}</c>", _suggestion.Word)));
                
                return items.ToArray();
            }
        }
    }
}
