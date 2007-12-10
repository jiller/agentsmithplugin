using System;
using System.Collections.Generic;
using AgentSmith.Comments;
using AgentSmith.NamingConventions;
using AgentSmith.Options;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith
{
    internal sealed class CodeAnalyzer : IRecursiveElementProcessor, IDisposable
    {
        private readonly IDaemonProcess _process;
        private readonly IDeclarationAnalyzer[] _analyzers;

        private readonly List<HighlightingInfo> _highlightings =
            new List<HighlightingInfo>();

        public CodeAnalyzer(IDaemonProcess process)
        {
            _process = process;
            CodeStyleSettings styleSettings = CodeStyleSettings.GetInstance(_process.Solution);
            _analyzers = new IDeclarationAnalyzer[]
                {
                    new NamingConventionsAnalyzer(styleSettings.NamingConventionSettings, _process.Solution),
                    //new ExceptionAnalyzer(_process.Solution, styleSettings.CatchOrSpecifySettings),
                    new CommentAnalyzer(styleSettings.CommentsSettings),
                    //new ClassStructureAnalyzer(styleSettings.MemberOrderSettings),
                    new SpellAnalyzer()
                };
        }

        public HighlightingInfo[] Highlightings
        {
            get
            {
                 return _highlightings.ToArray();
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            foreach (IDeclarationAnalyzer disposable in _analyzers)
            {
                disposable.Dispose();
            }
        }

        #endregion

        #region IRecursiveElementProcessor Members

        bool IRecursiveElementProcessor.ProcessingIsFinished
        {
            get { return _process.InterruptFlag; }
        }

        void IRecursiveElementProcessor.ProcessBeforeInterior(IElement element)
        {
            IDeclaration declaration = element as IDeclaration;
            if (declaration == null)
            {
                return;
            }

            foreach (IDeclarationAnalyzer analyzer in _analyzers)
            {
                HighlightingInfo[] highlightings = analyzer.Analyze(declaration);
                _highlightings.AddRange(highlightings);

                if (highlightings.Length > 0)
                {
                    return;
                }
            }
        }

        void IRecursiveElementProcessor.ProcessAfterInterior(IElement element)
        {
        }

        bool IRecursiveElementProcessor.InteriorShouldBeProcessed(IElement element)
        {
            return true;
        }

        #endregion
        
        public void Analyze(IElement element)
        {
            element.ProcessDescendants(this);
        }
    }
}