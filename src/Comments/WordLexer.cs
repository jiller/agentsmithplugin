using System;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.Util;

namespace AgentSmith.Comments
{
    public class WordLexer : ILexer
    {
        public class WordTokenType : TokenNodeType
        {
            public WordTokenType()
                : base("WORD")
            {
            }

            public override bool IsWhitespace
            {
                get { throw new NotImplementedException(); }
            }

            public override bool IsComment
            {
                get { throw new NotImplementedException(); }
            }

            public override bool IsStringLiteral
            {
                get { throw new NotImplementedException(); }
            }

            public override PsiLanguageType LanguageType
            {
                get { throw new NotImplementedException(); }
            }

            public override LeafElement Create(IBuffer buffer, int startOffset, int endOffset)
            {
                throw new NotImplementedException();
            }
        }

        private readonly string _data;
        private readonly TokenNodeType _word = new WordTokenType();
        private int _tokenStart = 0;
        private int _tokenEnd = 0;
        private TokenNodeType _tokenType;

        public WordLexer(string data)
        {
            _data = data;
        }

        #region ILexer Members

        public void Start()
        {
            Advance();
        }

        public void Advance()
        {
            _tokenStart = _tokenEnd;
            while (_tokenStart < _data.Length && isSeparator(_tokenStart))
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
            get { return _tokenEnd - 1; }
        }

        public string TokenText
        {
            get { return _data.Substring(_tokenStart, TokenEnd - TokenStart + 1); }
        }

        public IBuffer Buffer
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        private bool isSeparator(int i)
        {
            return !(char.IsLetterOrDigit(_data, i) || _data[i] == '\'' || _data[i] == '_') || _data[i] == '-';
        }
    }
}