using System;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith.Comments
{
    public static class XmlUtil
    {
        public static void SetDocComment(IDocCommentBlockOwnerNode docCommentBlockOwnerNode, string text, ISolution solution)
        {
            text = String.Format("///{0}\r\nclass Tmp {{}}", text.Replace("\n", "\n///"));

            ICSharpTypeMemberDeclaration declaration =
                CSharpElementFactory.GetInstance(docCommentBlockOwnerNode.GetPsiModule()).CreateTypeMemberDeclaration(text, new object[0]);
            ICSharpTypeMemberDeclarationNode node = declaration.ToTreeNode();
            docCommentBlockOwnerNode.SetDocCommentBlockNode(
                ((IDocCommentBlockOwnerNode)node).GetDocCommentBlockNode());
        }
    }
}
