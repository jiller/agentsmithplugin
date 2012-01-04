using System;

using AgentSmith.Options;

using JetBrains.Application;
using JetBrains.Application.Progress;
using JetBrains.Application.Settings;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.TextControl;
using JetBrains.Util;

namespace AgentSmith.Comments.Reflow
{
    [ContextAction(Group = "C#", Name = "Reflow & Retag Comment", Description = "Reflow & Retag Comment.")]
    internal class CommentReflowAndRetagAction : BulbItemImpl, IContextAction
    {

        protected readonly IContextActionDataProvider Provider;
        private IDocCommentNode _selectedDocCommentNode;


        public CommentReflowAndRetagAction(ICSharpContextActionDataProvider provider)
        {
            this.Provider = provider;
        }

        private int CalcLineOffset(IDocCommentBlockOwnerNode node)
        {
            ITreeNode prev = node.PrevSibling;
            if (prev != null && prev is IWhitespaceNode &&
                !((IWhitespaceNode)prev).IsNewLine)
            {
                return prev.GetTextLength();
            }
            return 0;
        }

        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            IDocCommentNode docCommentNode = this._selectedDocCommentNode;
            // Get the settings.
            IContextBoundSettingsStore settingsStore = Shell.Instance.GetComponent<ISettingsStore>().BindToContextTransient(ContextRange.ApplicationWide);
            XmlDocumentationSettings settings =
                settingsStore.GetKey<XmlDocumentationSettings>(SettingsOptimization.OptimizeDefault);
            int maxLength = settings.MaxCharactersPerLine;

            if (docCommentNode != null)
            {
                // Get the comment block owner (ie the part of the declaration which will own the comment).
                IDocCommentBlockNode blockNode =
                    docCommentNode.GetContainingNode<IDocCommentBlockNode>();
                if (blockNode == null) return null;
                IDocCommentBlockOwnerNode ownerNode =
                    blockNode.GetContainingNode<IDocCommentBlockOwnerNode>();

                // If we didn't get an owner then give up
                if (ownerNode == null) return null;

                // Get a factory which can create elements in the C# docs
                CSharpElementFactory factory = CSharpElementFactory.GetInstance(ownerNode.GetPsiModule());

                // Calculate line offset where /// starts and add 4 for the slashes and space
                int startPos = this.CalcLineOffset(ownerNode) + 4;

                // Create a new comment block with the adjusted text
                IDocCommentBlockNode comment = blockNode; //factory.CreateDocCommentBlock(text);

                string reflownText = new XmlCommentReflower(settings).ReflowAndRetag(comment, maxLength - startPos);

                comment = factory.CreateDocCommentBlock(reflownText);

                // And set the comment on the declaration.
                ownerNode.SetDocCommentBlockNode(comment);
            }

            return null;
        }

        public override string Text
        {
            get { return "Reflow & Retag Comment [Agent Smith]"; }
        }

        private T GetSelectedExpression<T>() where T : class, ITreeNode
        {
            return this.Provider.GetSelectedElement<T>(true, true);
        }

        public bool IsAvailable(IUserDataHolder cache)
        {
            using (ReadLockCookie.Create())
            {
                this._selectedDocCommentNode = this.GetSelectedExpression<IDocCommentNode>();

                return this._selectedDocCommentNode != null;
            }
        }
    }
}