using System;
using System.Windows.Forms;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Refactorings.Rename;
using JetBrains.ReSharper.Refactorings.RenameNamespace;
using JetBrains.ReSharper.Refactorings.Workflow;
using JetBrains.ReSharper.TextControl;
using JetBrains.Shell;
using JetBrains.Shell.Progress;
using JetBrains.Util;

namespace AgentSmith.NamingConventions
{    
    //TODO: rename this to something like IdentifierRenameBulbItem and move to appropriate place.
    public class NamingConventionsBulbItem : IBulbItem
    {
        private readonly IDeclaration _declaration;
        private readonly string _newName;
        private readonly object _syncObj = new object();

        public NamingConventionsBulbItem(IDeclaration declaration, string newName)
        {
            _newName = newName;
            _declaration = declaration;
        }

        #region IBulbItem Members

        public void Execute(ISolution solution, ITextControl textControl)
        {
            lock (_syncObj)
            {
                PsiManager psiManager = PsiManager.GetInstance(solution);
                if (psiManager.WaitForCaches())
                {
                    using (CommandCookie.Create("QuickFix: " + GetText()))
                    {
                        using (ModificationCookie modificationCookie = ensureWritable())
                        {
                            if (modificationCookie.EnsureWritableResult == EnsureWritableResult.SUCCESS)
                            {
                                IRefactoringWorkflow workflow = getRefactoringWorkflow(_declaration.DeclaredElement, _newName, textControl);
                                if (workflow != null)
                                {                                    
                                    PsiManager manager = PsiManager.GetInstance(solution);
                                    manager.DoTransaction(delegate { workflow.Execute(NullProgressIndicator.INSTANCE); });                                    
                                }
                            }
                        }
                    }
                }
            }
        }

        public string Text
        {
            get { return GetText(); }
        }

        #endregion

        private static IRefactoringWorkflow getRefactoringWorkflow(IDeclaredElement declaredElement, string newName,
                                                                   ITextControl textControl)
        {
            if (declaredElement is INamespace)
            {
                RenameNamespaceRefactoringWorkflow wf = new RenameNamespaceRefactoringWorkflow();
                if (wf.Initialize(new DataContext(null, declaredElement, textControl), null))
                {
                    wf.InitializeRefactoring(newName, NullProgressIndicator.INSTANCE, false);                    
                    return wf;
                }
                return null;
            }
            else
            {
                RenameRefactoringWorkflow wf = new RenameRefactoringWorkflow();
                if (wf.Initialize(new DataContext(null, declaredElement, textControl), null))
                {
                    wf.SetName(newName, NullProgressIndicator.INSTANCE, false);
                    if (wf.ConflictSearcher.SearchConflicts(NullProgressIndicator.INSTANCE).Conflicts.Count > 0)
                    {
                        MessageBox.Show("Conflicts were found. Can not rename.");
                        return null;
                    }
                    return wf;
                }
                return null;
            }            
        }

        public void ExecuteEx(ISolution solution, ITextControl textControl)
        {
            IRefactoringWorkflow wf = getRefactoringWorkflow(_declaration.DeclaredElement, _newName, textControl);
            if (wf != null)
            {                
                wf.Execute(NullProgressIndicator.INSTANCE);                
            }
        }

        private ModificationCookie ensureWritable()
        {
            HashSet<IProjectFile> set = new HashSet<IProjectFile>();
            IProjectFile projectFile = _declaration.GetContainingFile().ProjectItem;
            set.Add(projectFile);
            ISolution solution = projectFile.GetSolution();

            return solution.EnsureFilesWritable(set.ToArray());
        }

        protected string GetText()
        {
            return String.Format("Rename to '{0}'", _newName);
        }
    }
}