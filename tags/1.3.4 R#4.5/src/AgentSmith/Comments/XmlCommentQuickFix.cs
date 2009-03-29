using System;
using JetBrains.Application;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Intentions;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Util;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.TextControl;
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
            return _declaration.IsValid();
        }

        public IBulbItem[] Items
        {
            get { return new IBulbItem[] { new AddCommentBulbItem(_declaration) }; }
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

        #region IBulbItem Members

        public void Execute(ISolution solution, ITextControl textControl)
        {
            PsiManager psiManager = PsiManager.GetInstance(solution);
            if (psiManager.WaitForCaches("Agent Smith", "Cancel"))
            {
                using (CommandCookie.Create("QuickFix: " + Text))
                {
                    PsiManager manager = PsiManager.GetInstance(solution);
                    manager.DoTransaction(delegate { ExecuteEx(solution, textControl); });
                }
            }
        }

        public string Text
        {
            get { return "Add comment stub."; }
        }

        #endregion

        public void ExecuteEx(ISolution solution, ITextControl textControl)
        {
            IDocCommentBlockOwnerNode docCommentBlockOwnerNode =
                XmlDocTemplateUtil.FindDocCommentOwner(_declaration as ITypeMemberDeclaration);

            if (docCommentBlockOwnerNode != null)
            {
                int myCursorOffset;                
                string text = XmlDocTemplateUtil.GetDocTemplate(docCommentBlockOwnerNode, out myCursorOffset).Trim();
                
                XmlUtil.SetDocComment(docCommentBlockOwnerNode, text, solution);
            }
        }        
    }
}