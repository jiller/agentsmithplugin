using System;
using System.Text.RegularExpressions;
using AgentSmith.Options;
using AgentSmith.Test.Comments.Reflow;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Intentions.CSharp.ContextActions.Util;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.TextControl;
using JetBrains.Util;

namespace AgentSmith.Comments.Reflow
{
    //[ActionHandler(new string[] { "AgentSmith.CommentReflow" })]
    [ContextAction(Group = "C#", Name = "Reflow comment", Description = "Reflow comment.")]
    internal class CommentReflowAction : OneItemContextActionBase
    {
        public CommentReflowAction(ISolution solution, ITextControl textControl) : base(solution, textControl)
        {
        }

        protected override void ExecuteInternal()
        {
            IDocCommentNode docCommentNode = GetSelectedElement<IDocCommentNode>(false);
            if (docCommentNode != null)
            {                
                IDocCommentBlockNode blockNode = docCommentNode.GetContainingElement<IDocCommentBlockNode>(false);
                IDocCommentBlockOwnerNode ownerNode = blockNode.GetContainingElement<IDocCommentBlockOwnerNode>(false);
                if (ownerNode != null)
                {
                    Regex regex = new Regex("[ \t]*///");
                    string text = "///" + regex.Replace(blockNode.GetText(), "");                    
                    DocCommentBlockNode myBlockNode = new DocCommentBlockNode(new DocCommentNode(new MyNodeType(), new StringBuffer(text), 0, text.Length));
                    string reflownText = new XmlCommentReflower().Reflow(myBlockNode, CodeStyleSettings.GetInstance(Provider.Solution).CommentsSettings.MaxLineLength);                 
                    XmlUtil.SetDocComment(ownerNode, reflownText, Provider.Solution);
                }
            }
        }

        public override string Text
        {
            get { return "Reflow Comment [Agent Smith]"; }
        }
       
        protected override bool IsAvailableInternal()
        {            
            return GetSelectedElement<IDocCommentNode>(false) != null;            
        }       
    }
}