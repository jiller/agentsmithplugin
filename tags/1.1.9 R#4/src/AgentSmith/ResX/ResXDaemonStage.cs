using System;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp.Stages;

namespace AgentSmith.ResX
{
    [DaemonStage(StagesBefore = new Type[] { typeof(GlobalErrorStage) },
        StagesAfter = new Type[] { typeof(LanguageSpecificDaemonStage) }, RunForInvisibleDocument = true)]
    public class ResXDaemonStage : IDaemonStage
    {
        #region IDaemonStage Members

        public IDaemonStageProcess CreateProcess(IDaemonProcess process)
        {
            if (process.ProjectFile.Name.ToLower().EndsWith(".resx"))
            {
                return new ResXProcess(process.ProjectFile);
            }

            return null;
        }

        public ErrorStripeRequest NeedsErrorStripe(IProjectFile projectFile)
        {
            return ErrorStripeRequest.STRIPE_AND_ERRORS;
        }

        #endregion
    }
}