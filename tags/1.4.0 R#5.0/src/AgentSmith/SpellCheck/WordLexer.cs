using System;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.Text;
using JetBrains.Util;

namespace AgentSmith.SpellCheck
{
    /// <summary>
    /// Finds words in a string. Implements ReSharper's interface <see cref="ILexer"/>.
    /// </summary>
    public class WordLexer : ILexer
    {
        private readonly string _data;
        private readonly TokenNodeType _word = new WordTokenType();
        private int _tokenEnd = 0;
        private int _tokenStart = 0;
        private TokenNodeType _tokenType;
        private readonly StringBuffer _buffer;

        /// <summary>
        /// Initializes new instance.
        /// </summary>
        /// <param name="data">String to be tokenized.</param>
        public WordLexer(string data)
        {
            _data = data;
            _buffer = new StringBuffer(data);
        }

        #region ILexer Members

        public void Start()
        {
            Advance();
        }

        public void Advance()
        {
            _tokenStart = _tokenEnd;
            while (_tokenStart < _data.Length && (isSeparator(_tokenStart) || _data[_tokenStart] == '\''))
            {
                _tokenStart++;
            }
            _tokenEnd = _tokenStart + 1;
            while (_tokenEnd < _data.Length && !isSeparator(_tokenEnd))
            {
                _tokenEnd++;
            }
            if (_tokenEnd > _data.Length)
            {
                _tokenType = null;
            }
            else
            {
                _tokenType = _word;
            }

            while (_tokenStart <_data.Length && _tokenEnd > _tokenStart && _data[_tokenEnd - 1] == '\'')
            {
                _tokenEnd--;
            }
        }

        public object SaveState()
        {
            throw new NotImplementedException();
        }

        public void RestoreState(object state)
        {
            throw new NotImplementedException();
        }

        public TokenNodeType TokenType
        {
            get { return _tokenType; }
        }

        public int TokenStart
        {
            get { return _tokenStart; }
        }

        public int TokenEnd
        {
            get { return _tokenEnd; }
        }

        public string TokenText
        {
            get { return _data.Substring(_tokenStart, TokenEnd - TokenStart); }
        }
        
        public IBuffer Buffer
        {
            get { return _buffer; }
        }

        #endregion

        private bool isSeparator(int i)
        {
            return !(char.IsLetterOrDigit(_data, i) || _data[i] == '_' || _data[i] == '\'' || _data[i] == '&') || _data[i] == '-';
        }

        #region Nested type: WordTokenType

        public class WordTokenType : TokenNodeTypeBase
        {
            public WordTokenType()
                : base("WORD")
            {
            }
        }

        #endregion
    }
}