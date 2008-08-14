using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgentSmith.Comments
{
    public class LineBuilder
    {
        private StringBuilder sb = new StringBuilder();
        private string _currentLine;
        
        public string CurrentLine
        {
            get
            {
                return _currentLine;
            }
        }

        public void Append(string s)
        {                        
            int n = s.LastIndexOf("\n");
            if (n >= 0)
            {
                sb.Append(_currentLine);
                sb.Append(s.Substring(0, n + 1));
                _currentLine = s.Substring(n + 1);
            }
            else
            {
                _currentLine += s;
            }
        }

        public void AppendMultilineBlock(string block)
        {
            int n = block.IndexOf("\n");
            if (n < 0)
                n = block.Length;            

            Append(block);
        }

        public override string ToString()
        {
            return sb.ToString() + _currentLine;
        }
    }
}
