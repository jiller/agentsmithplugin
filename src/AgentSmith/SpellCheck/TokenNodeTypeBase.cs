using System;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.Text;

namespace AgentSmith.SpellCheck
{
    public class TokenNodeTypeBase: TokenNodeType
    {
        public TokenNodeTypeBase(string name) : base(name)
        {
        }

        public override bool IsWhitespace
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsComment
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsStringLiteral
        {
            get { throw new NotImplementedException(); }
        }

        public override PsiLanguageType LanguageType
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsIdentifier
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsKeyword
        {
            get { return false; }
        }

        public override LeafElementBase Create(IBuffer buffer, int startOffset, int endOffset)
        {
            throw new NotImplementedException();
        }
    }
}