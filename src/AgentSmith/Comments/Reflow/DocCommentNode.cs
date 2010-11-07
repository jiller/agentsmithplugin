using System;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.Text;

namespace AgentSmith.Comments.Reflow
{
    public class DocCommentNode : BindedLeafElement, IDocCommentNode
    {        
        public DocCommentNode(NodeType type, IBuffer buffer, TreeOffset startOffset, TreeOffset endOffset):
            base(type, buffer, startOffset, endOffset)
        {
        }

        public TokenNodeType GetTokenType()
        {
            throw new NotImplementedException();
        }

        public string CommentText
        {
            get { throw new NotImplementedException(); }
        }

        public CommentType CommentType
        {
            get { throw new NotImplementedException(); }
        }

        public IDocCommentNode ReplaceBy(IDocCommentNode docCommentNode)
        {
            throw new NotImplementedException();
        }
    }
}