using System;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Refactorings.Rename;
using JetBrains.ReSharper.Refactorings.RenameNamespace;
using JetBrains.ReSharper.Refactorings.Workflow;
using JetBrains.ReSharper.Refactorings.WorkflowW;
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
            RefactoringWorkflow refactoringWorkflow = getRefactoringWorkflow(solution, _declaration.DeclaredElement);
            if (refactoringWorkflow.Initialize(new DataContext(null, _declaration.DeclaredElement, textControl)))
            {
                new WorkflowProcessor(refactoringWorkflow, solution).ExecuteAction();
            }
        }

        public string Text
        {
            get { return "Rename..."; }
        }

        #endregion

        private static RefactoringWorkflow getRefactoringWorkflow(ISolution solution, IDeclaredElement declaredElement)
        {
            if (declaredElement is INamespace)
            {
                return new RenameNamespaceRefactoringWorkflow();
            }
            return new RenameWorkflow(solution);
        }
    }
}