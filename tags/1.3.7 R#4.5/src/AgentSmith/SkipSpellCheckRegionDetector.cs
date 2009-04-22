using System;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith
{
    public class SkipSpellCheckRegionDetector
    {        
        private int _inSkipSpellCheckRegion = 0;

        public bool InSkipSpellCheck
        {
            get { return (_inSkipSpellCheckRegion > 0); }
        }

        public void Process(IElement element)
        {
            ICSharpCommentNode commentNode = element.ToTreeNode() as ICSharpCommentNode;
            if (commentNode != null)
            {
                if (commentNode.CommentType == CommentType.END_OF_LINE_COMMENT && commentNode.CommentText != null)
                {                    
                    string text = commentNode.CommentText.Trim().ToLower();
                    if (text == "AgentSmith SpellCheck disable".ToLower())
                    {
                        _inSkipSpellCheckRegion++;
                    }
                    else if (text == "AgentSmith SpellCheck restore".ToLower())
                    {
                        _inSkipSpellCheckRegion--;
                    }
                }
            }                       
        }
    }
}
