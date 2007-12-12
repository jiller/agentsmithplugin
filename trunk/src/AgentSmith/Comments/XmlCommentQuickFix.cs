using System;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.CSharp.Util;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.TextControl;
using JetBrains.Shell;
using JetBrains.Util;

namespace AgentSmith.Comments
{
    [QuickFix]
    public class XmlCommentQuickFix : IQuickFix
    {
        private readonly IDeclaration _declaration;

        public XmlCommentQuickFix(FixCommentSuggestion suggestion)
        {
            _declaration = suggestion.Element as IDeclaration;
        }

        #region IQuickFix Members

        public bool IsAvailable(IUserDataHolder cache)
        {
            return true;
        }

        public IBulbItem[] Items
        {
            get { return new IBulbItem[] {new AddCommentBulbItem(_declaration)}; }
        }

        #endregion
    }

    internal class AddCommentBulbItem : IBulbItem
    {
        private readonly IDeclaration _declaration;

        public AddCommentBulbItem(IDeclaration declaration)
        {
            _declaration = declaration;
        }

        public void Execute(ISolution solution, ITextControl textControl)
        {
            PsiManager psiManager = PsiManager.GetInstance(solution);
            if (psiManager.WaitForCaches())
            {
                using (CommandCookie.Create("QuickFix: " + Text))
                {
                    PsiManager manager = PsiManager.GetInstance(solution);
                    manager.DoTransaction(delegate { ExecuteEx(solution, textControl); });
                }
            }
        }

        #region IBulbItem Members

        public string Text
        {
            get { return "Add comment stub."; }
        }

        public void ExecuteEx(ISolution solution, ITextControl textControl)
        {
            IDocCommentBlockOwnerNode docCommentBlockOwnerNode =
                XmlDocTemplateUtil.FindDocCommentOwner(_declaration as ITypeMemberDeclaration);

            if (docCommentBlockOwnerNode != null)
            {
                int myCursorOffset;
                string text = XmlDocTemplateUtil.GetDocTemplate(docCommentBlockOwnerNode, out myCursorOffset).Trim();
                text = "///" + text.Replace("\n", "\n///") + "\r\nclass Tmp {}";

                Logger.LogMessage(text);
                Logger.LogMessage("Set comment.");

                ICSharpTypeMemberDeclaration declaration =
                    CSharpElementFactory.GetInstance(solution).CreateTypeMemberDeclaration(text, new object[0]);
                ICSharpTypeMemberDeclarationNode node = declaration.ToTreeNode();
                docCommentBlockOwnerNode.SetDocCommentBlockNode(
                    ((IDocCommentBlockOwnerNode) node).GetDocCommentBlockNode());
            }
        }

        #endregion
    }
}