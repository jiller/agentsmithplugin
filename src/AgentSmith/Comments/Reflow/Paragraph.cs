using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgentSmith.Comments.Reflow
{
    public class Paragraph
    {
        private readonly List<ParagraphLine> _lines = new List<ParagraphLine>();

        public IList<ParagraphLine> Lines
        {
            get
            {
                return _lines;
            }
        }

        public void Add(ParagraphLine line)
        {
            _lines.Add(line);
        }
    }
}
