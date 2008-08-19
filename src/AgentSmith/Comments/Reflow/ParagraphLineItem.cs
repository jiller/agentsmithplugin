using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgentSmith.Comments.Reflow
{
    public enum ItemType
    {
        XmlSpace,
        XmlElement,
        NonReflowableBlock,
        Text
    }

    public class ParagraphLineItem
    {
        public string Text;
        public ItemType ItemType;

        public string FirstLine
        {
            get
            {
                int n = Text.IndexOf("\n");
                return n < 0? Text : Text.Substring(0, n);
            }
        }
    }
}
