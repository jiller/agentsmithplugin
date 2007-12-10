using System;
using System.Drawing;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith.Highlights
{
    internal class DeclarationIsInTheWrongPlace : SuggestionBase
    {
        public DeclarationIsInTheWrongPlace(IDeclaration element, string toolTip)
            : base(element, toolTip)
        {
        }

        public override Severity Severity
        {
            get { return Severity.INFO; }
        }

        public override Color ColorOnStripe
        {
            get { return Color.Purple; }
        }
    }
}