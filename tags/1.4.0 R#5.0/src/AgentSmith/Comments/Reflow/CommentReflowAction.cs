using System;
using System.Text.RegularExpressions;
using AgentSmith.Options;
using AgentSmith.Test.Comments.Reflow;
using JetBrains.ReSharper.Intentions;
using JetBrains.ReSharper.Intentions.CSharp.ContextActions.Util;
using JetBrains.ReSharper.Intentions.CSharp.DataProviders;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Text;


namespace AgentSmith.Comments.Reflow
{        
    [ContextAction(Group = "C#", Name = "Reflow comment", Description = "Reflow comment.")]
    internal class CommentReflowAction : OneItemContextActionBase
    {       

        public CommentReflowAction(ICSharpContextActionDataProvider provider) : base(provider)
        {           
        }

        protected override void ExecuteInternal(params object[] param)
        {
            IDocCommentNode docCommentNode = getSelectedElement<IDocCommentNode>();
            int maxLength = CodeStyleSettings.GetInstance(Provider.Solution).CommentsSettings.MaxLineLength;            

            if (docCommentNode != null)
            {                
                IDocCommentBlockNode blockNode =
                    docCommentNode.GetContainingElement<IDocCommentBlockNode>(false);
                IDocCommentBlockOwnerNode ownerNode =
                    blockNode.GetContainingElement<IDocCommentBlockOwnerNode>(false);
                if (ownerNode != null)
                {                    
                    Regex regex = new Regex("[ \t]*///");

                    string text = blockNode.GetText();

                    // Calculate line offset where /// starts and add 3 for each
                    // slash.
                    int startPos = CalcLineOffset(ownerNode) + 3;
                    text = "///" + regex.Replace( text , "");
                    DocCommentBlockNode myBlockNode =
                        new DocCommentBlockNode(new DocCommentNode(new MyNodeType(), new StringBuffer(text),
                                                                   TreeOffset.Zero, new TreeOffset(text.Length)));
                    string reflownText = new XmlCommentReflower().Reflow(myBlockNode, maxLength - startPos);                    
                    XmlUtil.SetDocComment(ownerNode, reflownText, Provider.Solution);
                }
            }            
        }

        protected override void ExecuteInternal()
        {
           
        }

        private int CalcLineOffset( IDocCommentBlockOwnerNode node )
        {
            ITreeNode prev = node.PrevSibling;
            if ( prev != null && prev is IWhitespaceNode &&
                 !( (IWhitespaceNode)prev ).IsNewLine )
            {
                return prev.GetTextLength();
            }
            else
            {
                return 0;
            }
        }
       
        public override string Text
        {
            get { return "Reflow Comment [Agent Smith]"; }
        }

        private T getSelectedElement<T>() where T : class, IElement
        {
            return myProvider.GetSelectedElement<T>(CheckDocumentRange, true) ;
        }
        protected override bool IsAvailableInternal()
        {
            return getSelectedElement<IDocCommentNode>() != null;           
        }
    }
}