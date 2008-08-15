using System;
using System.Collections.Generic;

namespace AgentSmith.Comments.Reflow
{
    public class ParagraphLine
    {
        private readonly List<ParagraphLineItem> _items = new List<ParagraphLineItem>();

        public void AddItem(ParagraphLineItem item)
        {
            _items.Add(item);
        }

        public IList<ParagraphLineItem> Items
        {
            get
            {
                return _items;
            }
        }

        public ParagraphLine TrimStart()
        {
            ParagraphLine newLine = new ParagraphLine();
            int i = 0;
            
            while (i < Items.Count && Items[i].ItemType == ItemType.Space)
                i++;
            
            while (i < Items.Count)
            {
                newLine.AddItem(Items[i]);
                i++;
            }

            return newLine;
        }

        public ParagraphLine TrimEnd()
        {
            ParagraphLine newLine = new ParagraphLine();
            int i = Items.Count - 1;

            while (i >=0 && Items[i].ItemType == ItemType.Space)
                i--;

            int j=0;
            while (j<=i)
            {
                newLine.AddItem(Items[j]);
                j++;
            }

            return newLine;
        }

        public ParagraphLine Trim()
        {
            return TrimStart().TrimEnd();
        }
    }
}
