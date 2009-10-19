using System;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;

namespace AgentSmith.Test.Comments.Reflow
{
    public class MyNodeType : NodeType
    {
        public MyNodeType() : base("MyNode")
        {
        }

        public override PsiLanguageType LanguageType
        {
            get { return new PsiLanguageType("CSharp"); }
        }
    }
}