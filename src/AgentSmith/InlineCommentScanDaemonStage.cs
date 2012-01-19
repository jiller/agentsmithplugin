using JetBrains.Application.Settings;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith
{
    /// <summary>
    /// A stage (process stage factory) which creates process stages for scanning inline (code) comments
    /// </summary>
    [DaemonStage(StagesBefore = new[] { typeof(LanguageSpecificDaemonStage) })]
    internal class InlineCommentScanDaemonStage : IDaemonStage
    {
        /// <summary>
        /// Check the error stripe indicator necessity for this stage after processing given <paramref name="sourceFile"/>
        /// </summary>
        public ErrorStripeRequest NeedsErrorStripe(IPsiSourceFile sourceFile, IContextBoundSettingsStore settingsStore)
        {
            return ErrorStripeRequest.STRIPE_AND_ERRORS;
        }

        /// <summary>
        /// Creates a code analysis process corresponding to this stage for analysing a file.
        /// </summary>
        /// <returns>
        /// Code analysis process to be executed or <c>null</c> if this stage is not available for this file.
        /// </returns>
        public IDaemonStageProcess CreateProcess(
            IDaemonProcess process, IContextBoundSettingsStore settings, DaemonProcessKind processKind)
        {
            IFile psiFile = process.SourceFile.GetPsiFile(CSharpLanguage.Instance);
            if (psiFile == null)
            {
                return null;
            }

            return new InlineCommentScanDaemonStageProcess(process, settings);
        }
    }
}