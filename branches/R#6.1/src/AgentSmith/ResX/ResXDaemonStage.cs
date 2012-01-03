using System;

using AgentSmith.Options;

using JetBrains.Application.Settings;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp.Stages;
using JetBrains.ReSharper.Psi;

namespace AgentSmith.ResX
{
    [DaemonStage(StagesBefore=new Type[] { typeof(UnsafeContextCheckingStage) })]
    public class ResXDaemonStage : IDaemonStage
    {
        #region IDaemonStage Members

        public IDaemonStageProcess CreateProcess(
            IDaemonProcess process,
            IContextBoundSettingsStore settingsStore,
            DaemonProcessKind processKind)
        {
            if (process.SourceFile.Name.ToLower().EndsWith(".resx"))
            {
                return new ResXProcess(process, settingsStore, process.SourceFile);
            }

            return null;
        }

        public ErrorStripeRequest NeedsErrorStripe(IPsiSourceFile sourceFile, IContextBoundSettingsStore settingsStore)
        {
            return ErrorStripeRequest.STRIPE_AND_ERRORS;
        }

        #endregion
    }
}