using System;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.CodeInsight.Services.CSharp.Generate.Util;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Util;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.TextControl;
using JetBrains.Shell;
using JetBrains.Util;

namespace AgentSmith.Comments
{
    [QuickFix]
    public class AddXmlCommentQuickFix : IQuickFix
    {
        private IDeclaration _declaration;

        public AddXmlCommentQuickFix(IDeclaration declaration)
        {
            _declaration = declaration;
        }

        #region IQuickFix Members

        public bool IsAvailable(IUserDataHolder cache)
        {
            return true;
        }

        public IBulbItem[] Items
        {
            get { return new IBulbItem[] {new BulbItem()}; }
        }

        #endregion

        #region Nested type: BulbItem

        public class BulbItem : IBulbItem
        {
            private readonly IDeclaration _declaration;

            public BulbItem(IDeclaration _declaration)
            {
                this._declaration = _declaration;
            }

            #region IBulbItem Members

            public void Execute(ISolution solution, ITextControl textControl)
            {
                using (CommandCookie.Create(Text))
                {
                    using (ModificationCookie ensureWritable = _declaration.GetDocumentRange().Document.EnsureWritable())
                    {
                        if (ensureWritable.EnsureWritableResult == EnsureWritableResult.SUCCESS)
                        {

                            IDocCommentBlockOwnerNode docCommentBlockOwnerNode = XmlDocTemplateUtil.FindDocCommentOwner(this.myDeclaration as ITypeMemberDeclaration);
                             IDocCommentBlockNode commentBlockNode = docCommentBlockOwnerNode.GetDocCommentBlockNode();
                            string text; 
                            if (commentBlockNode == null)
                             {
                                 text =
                                     XmlDocTemplateUtil.GetDocTemplate(docCommentBlockOwnerNode, out this.myCursorOffset);
                             }
                        }
                    }
                }
            }

            public string Text
            {
                get { return "Add comment stub."; }
            }

            #endregion
        }

        #endregion
    }
}