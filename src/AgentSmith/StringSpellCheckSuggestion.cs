using System;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Editor;

namespace AgentSmith
{
    public class StringSpellCheckSuggestion : SuggestionBase
    {
        public StringSpellCheckSuggestion(DocumentRange range) : base(range, "Word is not in dictionary.")
        {
        }

        public override Severity Severity
        {
            get { return Severity.INFO; }
        }

        public override string AttributeId
        {
            get { return "ReSharper Suggestion"; }
        }
    }
}