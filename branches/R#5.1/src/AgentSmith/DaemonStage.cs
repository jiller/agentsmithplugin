using System;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp.Stages;
using IDaemonProcess=JetBrains.ReSharper.Daemon.IDaemonProcess;
using IDaemonStageProcess=JetBrains.ReSharper.Daemon.IDaemonStageProcess;
using LanguageSpecificDaemonStage=JetBrains.ReSharper.Daemon.LanguageSpecificDaemonStage;

namespace AgentSmith
{
    /// <summary>
    /// Agent Smith stage.
    /// </summary>
    [DaemonStage(StagesBefore = new Type[] { typeof(UnsafeContextCheckingStage) },
        StagesAfter = new Type[] { typeof(LanguageSpecificDaemonStage) })]
    public class DaemonStage : CSharpDaemonStageBase
    {
        public override IDaemonStageProcess CreateProcess(IDaemonProcess process, DaemonProcessKind kind)
        {
            //TODO: implement aspx file checking later.
            if (!IsSupported(process.ProjectFile) ||
                !process.ProjectFile.Name.ToLower().EndsWith(".cs"))
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