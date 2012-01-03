using System.Text.RegularExpressions;

using AgentSmith.Options;

using JetBrains.Application;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
using JetBrains.ReSharper.Intentions.CSharp.ContextActions.Util;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

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

            // Get the settings.
            IContextBoundSettingsStore settingsStore = Shell.Instance.GetComponent<ISettingsStore>().BindToContextTransient(ContextRange.ApplicationWide);
            XmlDocumentationSettings settings =
                settingsStore.GetKey<XmlDocumentationSettings>(SettingsOptimization.OptimizeDefault);
            int maxLength = settings.MaxCharactersPerLine;
                
            if (docCommentNode != null)
            {
                // Get the comment block owner (ie the part of the declaration which will own the comment).
                IDocCommentBlockNode blockNode =
                    docCommentNode.GetContainingNode<IDocCommentBlockNode>(false);
                IDocCommentBlockOwnerNode ownerNode =
                    blockNode.GetContainingNode<IDocCommentBlockOwnerNode>(false);

                // If we didn't get an owner then give up
                if (ownerNode == null) return;
                
                Regex regex = new Regex("[ \t]*///[ ]?");

                string text = blockNode.GetText();

                // Get a factory which can create elements in the C# docs
                CSharpElementFactory factory = CSharpElementFactory.GetInstance(ownerNode.GetPsiModule());

                // Calculate line offset where /// starts and add 3 for each
                // slash.
                int startPos = this.CalcLineOffset(ownerNode) + 3;

                //text = regex.Replace( text , "");
                /*DocCommentBlockNode myBlockNode =
                    new DocCommentBlockNode(new DocCommentNode(new MyNodeType(), new StringBuffer(text),
                        TreeOffset.Zero, new TreeOffset(text.Length)));

                 * */

                // Create a new comment block with the adjusted text
                IDocCommentBlockNode comment = blockNode; //factory.CreateDocCommentBlock(text);

                string reflownText = new XmlCommentReflower().Reflow(comment, maxLength - startPos);

                comment = factory.CreateDocCommentBlock(reflownText);

                // And set the comment on the declaration.
                ownerNode.SetDocCommentBlockNode(comment);
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

        private T getSelectedElement<T>() where T : class, ITreeNode
        {
            return myProvider.GetSelectedElement<T>(CheckDocumentRange, true) ;
        }
        protected override bool IsAvailableInternal()
        {
            return getSelectedElement<IDocCommentNode>() != null;           
        }
    }

    [ContextAction(Group = "C#", Name = "Reflow & Retag comment", Description = "Reflow & Retag comment.")]
    internal class CommentReflowAndRetagAction : OneItemContextActionBase
    {

        public CommentReflowAndRetagAction(ICSharpContextActionDataProvider provider)
            : base(provider)
        {
        }

        protected override void ExecuteInternal(params object[] param)
        {
            IDocCommentNode docCommentNode = getSelectedElement<IDocCommentNode>();
            // Get the settings.
            IContextBoundSettingsStore settingsStore = Shell.Instance.GetComponent<ISettingsStore>().BindToContextTransient(ContextRange.ApplicationWide);
            XmlDocumentationSettings settings =
                settingsStore.GetKey<XmlDocumentationSettings>(SettingsOptimization.OptimizeDefault);
            int maxLength = settings.MaxCharactersPerLine;

            if (docCommentNode != null)
            {
                // Get the comment block owner (ie the part of the declaration which will own the comment).
                IDocCommentBlockNode blockNode =
                    docCommentNode.GetContainingNode<IDocCommentBlockNode>(false);
                IDocCommentBlockOwnerNode ownerNode =
                    blockNode.GetContainingNode<IDocCommentBlockOwnerNode>(false);

                // If we didn't get an owner then give up
                if (ownerNode == null) return;

                // Get a factory which can create elements in the C# docs
                CSharpElementFactory factory = CSharpElementFactory.GetInstance(ownerNode.GetPsiModule());

                // Calculate line offset where /// starts and add 4 for the slashes and space
                int startPos = this.CalcLineOffset(ownerNode) + 4;

                // Create a new comment block with the adjusted text
                IDocCommentBlockNode comment = blockNode; //factory.CreateDocCommentBlock(text);

                string reflownText = new XmlCommentReflower().ReflowAndRetag(comment, maxLength - startPos);

                comment = factory.CreateDocCommentBlock(reflownText);

                // And set the comment on the declaration.
                ownerNode.SetDocCommentBlockNode(comment);
            }
        }

        protected override void ExecuteInternal()
        {

        }

        private int CalcLineOffset(IDocCommentBlockOwnerNode node)
        {
            ITreeNode prev = node.PrevSibling;
            if (prev != null && prev is IWhitespaceNode &&
                 !((IWhitespaceNode)prev).IsNewLine)
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
            get { return "Reflow & Retag Comment [Agent Smith]"; }
        }

        private T getSelectedElement<T>() where T : class, ITreeNode
        {
            return myProvider.GetSelectedElement<T>(CheckDocumentRange, true);
        }
        protected override bool IsAvailableInternal()
        {
            return getSelectedElement<IDocCommentNode>() != null;
        }
    }

}