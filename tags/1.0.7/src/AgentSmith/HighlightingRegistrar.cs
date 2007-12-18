using System;
using AgentSmith.Comments;
using AgentSmith.NamingConventions;
using JetBrains.ComponentModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.Shell;

namespace AgentSmith
{
    /// <summary>
    /// Registers Agent Smith highlighters.
    /// </summary>
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
            string group = "Agent Smith";
            manager.RegisterConfigurableSeverity(WordIsNotInDictionarySuggestion.NAME, group, "Word doesn't exist in dictionary.", "Word doesn't exist in default or user dictionary.", Severity.SUGGESTION);
            manager.RegisterConfigurableSeverity(FixCommentSuggestion.NAME, group, "Public members must have XML comment.", "Public members must have XML comments.", Severity.SUGGESTION);
            manager.RegisterConfigurableSeverity(NamingConventionsSuggestion.NAME, group, "Declarations must conform to naming conventions.", "Declarations must conform to naming conventions", Severity.WARNING);
        }

        #endregion
    }
}