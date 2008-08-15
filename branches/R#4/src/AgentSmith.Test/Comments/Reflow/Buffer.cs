using System;
using System.Text;
using JetBrains.Util;

namespace AgentSmith.Test.Comments.Reflow
{
    class Buffer : IBuffer
    {
        private string _text;
        
        public Buffer(string text)
        {
            _text = text;
        }

        #region IBuffer Members

        public void AppendTextTo(StringBuilder builder, TextRange from)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(int sourceIndex, char[] destinationArray, int destinationIndex, int length)
        {
            throw new NotImplementedException();
        }

        public string GetText(TextRange from)
        {
            return _text.Substring(from.StartOffset, from.EndOffset - from.StartOffset);
        }

        public string GetText()
        {
            throw new NotImplementedException();
        }

        public int Length
        {
            get { return _text.Length; }
        }

        public char this[int i]
        {
            get { return _text[i]; }
        }

        #endregion
    }
}
