using System;
using AgentSmith.Comments;
using AgentSmith.NamingConventions;
using AgentSmith.Options;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp.Stages;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith
{
    public class DaemonProcess : CSharpDaemonStageProcessBase
    {
        private readonly IDeclarationAnalyzer[] _analyzers;
        private readonly IDaemonProcess _process;

        public DaemonProcess(IDaemonProcess daemonProcess)
            : base(daemonProcess)
        {
            _process = daemonProcess;
            CodeStyleSettings styleSettings = CodeStyleSettings.GetInstance(_process.Solution);
            _analyzers = new IDeclarationAnalyzer[]
                {
                    new NamingConventionsAnalyzer(styleSettings.NamingConventionSettings, _process.Solution),
                    //new ExceptionAnalyzer(_process.Solution, styleSettings.CatchOrSpecifySettings),
                    new CommentAnalyzer(styleSettings.CommentsSettings, _process.Solution),
                    //new ClassStructureAnalyzer(styleSettings.MemberOrderSettings),
                    //new SpellAnalyzer()
                };
        }
        
        public override void ProcessFile(ICSharpFile file)
        {
            file.ProcessDescendants(this);
            FullyRehighlighted = true;
        }

        public override void ProcessBeforeInterior(IElement element)
        {
            base.ProcessBeforeInterior(element);
            IDeclaration declaration = element as IDeclaration;
            if (declaration == null)
            {
                return;
            }

            foreach (IDeclarationAnalyzer analyzer in _analyzers)
            {
                foreach (CSharpHighlightingBase highlighting in analyzer.Analyze(declaration))
                {
                   AddHighlighting(highlighting);                   
                }
            }
        }
    }
}