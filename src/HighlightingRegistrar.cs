using System;
using AgentSmith.Comments;
using AgentSmith.NamingConventions;
using JetBrains.ComponentModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.Shell;

namespace AgentSmith
{
    [ShellComponentImplementation(ProgramConfigurations.ALL)]
    public class HighlightingRegistrar : IShellComponent
    {
        #region IShellComponent Members

        public void Dispose()
        {
        }

        public void Init()
        {
            HighlightingSettingsManager manager = HighlightingSettingsManager.Instance;
            manager.RegisterConfigurableSeverity(WordIsNotInDictionarySuggestion.NAME, "Agent Smith", "Word doesn't exist in dictionary.", "Word doesn't exist in default or user dictionary.", Severity.SUGGESTION);
            manager.RegisterConfigurableSeverity(FixCommentSuggestion.NAME, "Agent Smith", "Public members must have XML comment.", "Public members must have XML comments.", Severity.SUGGESTION);
            manager.RegisterConfigurableSeverity(NamingConventionsSuggestion.NAME, "Agent Smith", "Declarations must conform to naming conventions.", "Declarations must conform to naming conventions", Severity.WARNING);
        }

        #endregion
    }
}