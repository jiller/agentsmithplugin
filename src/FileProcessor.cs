using System;
using System.Collections.Generic;
using AgentSmith.Comments;
using AgentSmith.NamingConventions;
using AgentSmith.Options;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.CodeView.Search;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith
{
    internal class FileProcessor : IRecursiveElementProcessor, IDisposable
    {
        private IDictionary<IDeclarationAnalyzer,  SearchResultsConsumer> _analyzers = new Dictionary<IDeclarationAnalyzer, SearchResultsConsumer>(); 
        
        public FileProcessor(FindResultsProvider provider, ISolution solution)
        {
        CodeStyleSettings styleSettings = CodeStyleSettings.GetInstance(solution);

            _analyzers.Add(new ExceptionAnalyzer(solution, styleSettings.CatchOrSpecifySettings), provider.Exceptions);
            _analyzers.Add(new NamingConventionsAnalyzer(styleSettings.NamingConventionSettings, solution), provider.NamingConventions);
            _analyzers.Add(new CommentAnalyzer(styleSettings.CommentsSettings), provider.Comments);
            _analyzers.Add(new ClassStructureAnalyzer(styleSettings.MemberOrderSettings), provider.Structure);
        }


        public void Dispose()
        {
            foreach (IDisposable key in _analyzers.Keys)
            {
                key.Dispose();
            }
        }

        public bool InteriorShouldBeProcessed(IElement element)
        {
            return true;
        }

        public void ProcessFile(ICSharpFile file)
        {
            if (file != null)
            {
                file.ProcessDescendants(this);
            }
        }
        
        public void ProcessBeforeInterior(IElement element)
        {
            if (element is IDeclaration && ((IDeclaration)element).DeclaredElement !=null)
            {
                IDeclaration decl = (IDeclaration) element;
                foreach (KeyValuePair<IDeclarationAnalyzer, SearchResultsConsumer> pair in _analyzers)
                {
                    IHighlighting[] suggestions = pair.Key.Analyze(decl);

                    if (suggestions.Length > 0)
                    {
                        try
                        {
                            FindResultDeclaredElement result = new FindResultDeclaredElement(((IDeclaration)element).DeclaredElement);
                            pair.Value.ConsumeResult(result);
                        }
                        catch
                        {}
                    }
                }                                
            }
        }

        public void ProcessAfterInterior(IElement element)
        {
        }

        public bool ProcessingIsFinished
        {
            get { return false; }
        }                
    }
}