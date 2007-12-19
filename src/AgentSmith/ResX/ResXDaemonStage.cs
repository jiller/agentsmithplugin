using System;
using AgentSmith.Resx;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp.Stages;

namespace AgentSmith.ResX
{
    /// <summary>
    /// Open office dictionaries: http://wiki.services.openoffice.org/wiki/Dictionaries
    /// </summary>
    [DaemonStage(StagesBefore = new Type[] {typeof (GlobalErrorStage)},
        StagesAfter = new Type[] {typeof (LanguageSpecificDaemonStage)}, RunForInvisibleDocument = true)]
    public class ResXDaemonStage : IDaemonStage
    {
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
    }
}