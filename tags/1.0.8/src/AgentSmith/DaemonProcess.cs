using System;
using System.Collections.Generic;
using AgentSmith.Comments;
using AgentSmith.NamingConventions;
using AgentSmith.Options;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Shell.Progress;

namespace AgentSmith
{
    public class DaemonProcess : ElementVisitor, IDaemonStageProcess, IRecursiveElementProcessor
    {
        private readonly IDeclarationAnalyzer[] _analyzers;
        private readonly IDaemonProcess _process;
        private readonly List<HighlightingInfo> _highlightings = new List<HighlightingInfo>();

        public DaemonProcess(IDaemonProcess daemonProcess)            
        {
            _process = daemonProcess;
            CodeStyleSettings styleSettings = CodeStyleSettings.GetInstance(_process.Solution);
            _analyzers = new IDeclarationAnalyzer[]
                {
                    new NamingConventionsAnalyzer(styleSettings.NamingConventionSettings, _process.Solution),
                    new CommentAnalyzer(styleSettings.CommentsSettings, _process.Solution),                    
                };
        }


        public DaemonStageProcessResult Execute()
        {
            DaemonStageProcessResult result = new DaemonStageProcessResult();
            ICSharpFile myFile = (ICSharpFile)PsiManager.GetInstance(_process.Solution).GetPsiFile(_process.ProjectFile);
            ProcessFile(myFile);
            result.FullyRehighlighted = true;
            
            result.Highlightings = _highlightings.ToArray();
            return result;
        }

        public  void ProcessFile(ICSharpFile file)
        {
            file.ProcessDescendants(this);            
        }

        public void ProcessBeforeInterior(IElement element)
        {
            IDeclaration declaration = element as IDeclaration;
            if (declaration == null)
            {
                return;
            }

            foreach (IDeclarationAnalyzer analyzer in _analyzers)
            {
                foreach (SuggestionBase highlighting in analyzer.Analyze(declaration))
                {
                   addHighlighting(highlighting);                   
                }
            }
        }

        private void addHighlighting(SuggestionBase highlighting)
        {
            _highlightings.Add(new HighlightingInfo(highlighting.Range, highlighting));
        }


        public bool InteriorShouldBeProcessed(IElement element)
        {
            return true;
        }

        public void ProcessAfterInterior(IElement element)
        {            
        }

        public bool ProcessingIsFinished
        {
            get
            {
                if (_process.InterruptFlag)
                {
                    throw new ProcessCancelledException();
                }
                return false;                
            }
        }
    }
}