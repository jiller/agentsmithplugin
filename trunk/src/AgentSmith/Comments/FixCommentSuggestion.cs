using System;
using AgentSmith.MemberMatch;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith.Comments
{
    [ConfigurableSeverityHighlighting(NAME)]
    public class FixCommentSuggestion : SuggestionBase
    {
        public const string NAME = "PublicMembersMustHaveComments";

        public FixCommentSuggestion(IDeclaration declaration, Match match)
            : base(NAME, declaration, declaration.GetNameDocumentRange(), match + "should have XML comment.")
        {
        }        
    }
}