using System;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Refactorings.Workflow;
using JetBrains.TextControl;

namespace AgentSmith.NamingConventions
{
    internal class RenameBulbItem : IBulbItem
    {
        private readonly IDeclaration _declaration;

        public RenameBulbItem(IDeclaration declaration)
        {
            _declaration = declaration;
        }

        #region IBulbItem Members

        public void Execute(ISolution solution, ITextControl textControl)
        {
            IDeclaredElement declaredElement = _declaration.DeclaredElement;
            if (declaredElement != null)
            {
                RefactoringWorkflow refactoringWorkflow = RefactoringWorkflow.GetRefactoringWorkflow(declaredElement,
                                                                                                     solution);
                if (refactoringWorkflow.Initialize(new DataContext(null, _declaration.DeclaredElement, textControl,
                                                                   solution, null)))
                {
                    new WorkflowProcessor(refactoringWorkflow, solution).ExecuteAction();
                }
            }
        }

        public string Text
        {
            get { return "Rename..."; }
        }

        #endregion       
    }
}