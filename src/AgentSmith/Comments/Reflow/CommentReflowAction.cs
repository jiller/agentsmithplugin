using System;
using System.Text.RegularExpressions;
using AgentSmith.Options;
using AgentSmith.Test.Comments.Reflow;
using JetBrains.Application;
using JetBrains.ReSharper.Intentions;
using JetBrains.ReSharper.Intentions.CSharp.ContextActions;
using JetBrains.ReSharper.Intentions.CSharp.ContextActions.Util;
using JetBrains.ReSharper.Psi.CSharp.Parsing;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Text;
using JetBrains.Util;

namespace AgentSmith.Comments.Reflow
{    
    [ContextAction(Group = "C#", Name = "Reflow comment", Description = "Reflow comment.")]
    internal class CommentReflowAction : OneItemContextActionBase
    {       

        public CommentReflowAction(ICSharpContextActionDataProvider provider) : base(provider)
        {           
        }

        protected override void ExecuteInternal()
        {
            IDocCommentNode docCommentNode = GetSelectedElement<IDocCommentNode>(false);
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
                    string text = "///" + regex.Replace(blockNode.GetText(), "");
                    DocCommentBlockNode myBlockNode =
                        new DocCommentBlockNode(new DocCommentNode(new MyNodeType(), new StringBuffer(text),
                                                                   0, text.Length));
                    string reflownText = new XmlCommentReflower().Reflow(myBlockNode, maxLength);
                    XmlUtil.SetDocComment(ownerNode, reflownText, Provider.Solution);
                }
            }            
        }

        protected override object[] ExecuteBeforeTransaction(object[] data, out bool needContinue)
        {
            int maxLength = CodeStyleSettings.GetInstance(Provider.Solution).CommentsSettings.MaxLineLength;
            needContinue = true;
            ICommentNode commentNode = GetSelectedElement<ICommentNode>(false);
            if (commentNode != null && !(commentNode is IDocCommentNode))
            {
                needContinue = false;
                using (CommandCookie.Create(Text))
                {
                    using (ModificationCookie ensureWritable = Provider.Document.EnsureWritable())
                    {
                        if (ensureWritable.EnsureWritableResult == EnsureWritableResult.SUCCESS)
                        {
                            if (commentNode.GetTokenType() == CSharpTokenType.C_STYLE_COMMENT)
                            {
                                string text = "///" + commentNode.GetText();
                                var node = new DocCommentNode(new MyNodeType(), new StringBuffer(text), 0,
                                                              text.Length);
                                DocCommentBlockNode myBlockNode = new DocCommentBlockNode(node);
                                string reflownText = new XmlCommentReflower().Reflow(myBlockNode, maxLength);
                                Provider.Document.ReplaceText(commentNode.GetDocumentRange().TextRange, reflownText);
                            }
                        }
                    }
                }
            }

            return new object[0];
        }        

        public override string Text
        {
            get { return "Reflow Comment [Agent Smith]"; }
        }

        protected override bool IsAvailableInternal()
        {
            return GetSelectedElement<IDocCommentNode>(false) != null;
                //|| GetSelectedElement<ICommentNode>(false) != null;
        }
    }
}