using System;
using AgentSmith.Options;
using AgentSmith.SpellCheck;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Editor;

namespace AgentSmith.Strings
{
    [ConfigurableSeverityHighlighting(NAME)]
    public class StringSpellCheckSuggestion : SpellCheckSuggestionBase
    {
        public const string NAME = "StringLiteralsWordIsNotInDictionary";

        public StringSpellCheckSuggestion(DocumentRange range, string word, ISolution solution, CommentsSettings settings)
            : base(NAME, range, word, solution, settings)
        {
        }

        public override Severity Severity
        {
            get
            {
                Severity severity = HighlightingSettingsManager.Instance.Settings.GetSeverity(NAME);
                return severity == JetBrains.ReSharper.Daemon.Severity.DO_NOT_SHOW ? severity : Severity.INFO;
            }
        }

        public override string AttributeId
        {
            get
            {
                return HighlightingAttributeIds.GetDefaultAttribute(Severity.SUGGESTION);                
            }
        }

        public static bool Enabled
        {
            get
            {
                return HighlightingSettingsManager.Instance.Settings.GetSeverity(NAME) !=
                       Severity.DO_NOT_SHOW;
            }
        }
    }
}