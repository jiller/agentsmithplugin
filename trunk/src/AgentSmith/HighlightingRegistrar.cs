using System;
using AgentSmith.Comments;
using AgentSmith.NamingConventions;
using AgentSmith.Resx;
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

            manager.RegisterConfigurableSeverity(WordIsNotInDictionarySuggestion.NAME, group,
                "Word found in XML comment doesn't exist in dictionary.",
                "Spell checking of XML comments have found a word that doesn't exist in default or user dictionary and is probably misspelled.",
                Severity.SUGGESTION);
            
            manager.RegisterConfigurableSeverity(CanBeSurroundedWithMetatagsSuggestion.NAME, group,
                "Word can be surrounded with meta tags.",
                "A word in a XML comment seems to be an identifier and can be surrounded by &lt;see cref=''..> or &lt;paramref ..> or similar tags to appear as link in documentation.",
                Severity.SUGGESTION);
            
            manager.RegisterConfigurableSeverity(FixCommentSuggestion.NAME, group,
                "Members must have XML comment.",
                "Class members configured in Agent Smith settings must have XML comments.",
                Severity.SUGGESTION);

            manager.RegisterConfigurableSeverity(NamingConventionsSuggestion.NAME, group,
                "Declarations must conform to naming conventions.",
                "Declarations must conform to naming conventions configured in Agent Smith settings.",
                Severity.WARNING);

            manager.RegisterConfigurableSeverity(ResXSpellHighlighting.NAME, group,
                "Word found in ResX file doesn't exist in dictionary.",
                "Spell checking of strings in a ResX file found a word that doesn't exist in default or user dictionary and is probably misspelled.",
                Severity.WARNING);
        }

        #endregion
    }
}