using System;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp.Stages;

namespace AgentSmith
{
    /// <summary>
    /// Agent Smith stage.
    /// </summary>
    [DaemonStage(StagesBefore = new Type[] { typeof(GlobalErrorStage) },
        StagesAfter = new Type[] { typeof(LanguageSpecificDaemonStage) }, RunForInvisibleDocument = true)]
    public class DaemonStage : CSharpDaemonStageBase
    {
        public override IDaemonStageProcess CreateProcess(IDaemonProcess process)
        {
            //TODO: implement aspx file checking later.
            if (!IsSupported(process.ProjectFile) ||
                process.ProjectFile.Name.ToLower().EndsWith(".aspx"))
            {
                return null;
            }
            return new DaemonProcess(process);
        }

        public override ErrorStripeRequest NeedsErrorStripe(IProjectFile projectFile)
        {
            return ErrorStripeRequest.STRIPE_AND_ERRORS;
        }
    }
}