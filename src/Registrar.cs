using System;
using JetBrains.ComponentModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.Shell;

namespace AgentSmith
{
    [ShellComponentImplementation(ProgramConfigurations.ALL)]
    public class SeverityRegistrarComponent : IShellComponent
    {
        #region IShellComponent Members

        public void Dispose()
        {
        }

        public void Init()
        {
            HighlightingSettingsManager manager = HighlightingSettingsManager.Instance;
            manager.RegisterConfigurableSeverity("Hello", "Spell check", "Word doesn't exist", "Word doesn't exist.", Severity.WARNING);
        }

        #endregion
    }
}