using System;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Text;
using JetBrains.Util;

namespace AgentSmith.Comments
{
    public class XmlDocLexer : ILexer
    {
        public readonly XmlTokenTypes XmlTokenType = XmlTokenTypeFactory.GetTokenTypes(PsiLanguageType.UNKNOWN);
        private readonly IDocCommentBlockNode _myDocCommentBlock;
        private IDocCommentNode _myCurrentCommentNode;
        private XmlLexerGenerated _myLexer;

        public XmlDocLexer(IDocCommentBlockNode docCommentBlock)
        {
            _myDocCommentBlock = docCommentBlock;
            Start();
        }

        public IDocCommentNode CurrentCommentNode
        {
            get { return _myCurrentCommentNode; }
        }

        public TextRange TokenLocalRange
        {
            get
            {
                if (_myCurrentCommentNode == null)
                {
                    return TextRange.InvalidRange;
                }
                BindedLeafElement leaf = (BindedLeafElement)_myCurrentCommentNode;
                int offset = leaf.Offset - leaf.GetDocumentRange().TextRange.StartOffset;
                return new TextRange(TokenStart - offset, TokenEnd - offset);
            }
        }

        #region ILexer Members

        public void Advance()
        {
            if (_myCurrentCommentNode != null)
            {
                uint state = _myLexer.LexerState;
                _myLexer.Advance();                
                if (_myLexer.TokenType == null)
                {
                    restartLexer(_myCurrentCommentNode.NextSibling, state);                    
                }                
            }
        }

        public void RestoreState(object state)
        {
            throw new InvalidOperationException();
        }

        public object SaveState()
        {
            throw new InvalidOperationException();
        }

        public void Start()
        {
            _myCurrentCommentNode = null;
            restartLexer(_myDocCommentBlock.FirstChild, 0);
        }

        public IBuffer Buffer
        {
            get
            {
                if (_myLexer != null)
                {
                    return _myLexer.Buffer;
                }
                return null;
            }
        }

        public int TokenEnd
        {
            get
            {
                if (_myLexer != null)
                {
                    return _myLexer.TokenEnd;
                }
                return -1;
            }
        }

        public int TokenStart
        {
            get
            {
                if (_myLexer != null)
                {
                    return _myLexer.TokenStart;
                }
                return -1;
            }
        }

        public string TokenText
        {
            get
            {
                if (_myLexer != null)
                {
                    return _myLexer.TokenText;
                }
                return null;
            }
        }

        public TokenNodeType TokenType
        {
            get
            {
                if (_myLexer != null)
                {
                    return _myLexer.TokenType;
                }
                return null;
            }
        }

        #endregion

        private void restartLexer(ITreeNode child, uint state)
        {
            _myCurrentCommentNode = null;
            while (child != null)
            {
                _myCurrentCommentNode = child as IDocCommentNode;
                if (_myCurrentCommentNode != null)
                {
                    break;
                }
                child = child.NextSibling;
            }
            if (_myCurrentCommentNode != null)
            {
                BindedLeafElement leaf = (BindedLeafElement)_myCurrentCommentNode;
                _myLexer = new XmlLexerGenerated(leaf.Buffer);
                _myLexer.Start(leaf.Offset + 3, leaf.Offset + leaf.Length, state);
                if (_myLexer.TokenType == null)
                {
                    restartLexer(_myCurrentCommentNode.NextSibling, state);
                }
            }
            else
            {
                _myLexer = null;
            }
        }
    }
}