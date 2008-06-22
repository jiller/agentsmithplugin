using System;
using JetBrains.Application;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Refactorings.PostRename.PostRenameModel;
using JetBrains.ReSharper.Refactorings.Rename;
using JetBrains.ReSharper.Refactorings.Workflow;
using JetBrains.TextControl;
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
                if (psiManager.WaitForCaches("Agent Smith", "Cancel"))
                {
                    using (CommandCookie.Create("QuickFix: " + GetText()))
                    {
                        using (ModificationCookie modificationCookie = ensureWritable())
                        {
                            if (modificationCookie.EnsureWritableResult == EnsureWritableResult.SUCCESS)
                            {
                                ExecuteEx(solution, textControl);                                
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

        private class RenameDataProvider : IRenameDataProvider
        {
            private readonly string _oldName;
            private readonly string _newName;
            public RenameDataProvider(string oldName, string newName)
            {
                _oldName = oldName;
                _newName = newName;
            }
            public NameChanges GetBaseNameChanges()
            {
                return new NameChanges(_oldName, _newName);
            }
            public string Name
            {
                get { return _newName; }
            }
            public bool DoRenameFile
            {
                get { return true; }
            }
            public bool ChangeText
            {
                get { return false; }
            }
        }       

        public void ExecuteEx(ISolution solution, ITextControl textControl)
        {
            IDeclaredElement declaredElement = _declaration.DeclaredElement;
            if (declaredElement != null)
            {
                RefactoringWorkflow wf = RefactoringWorkflow.GetRefactoringWorkflow(declaredElement, solution);

                string oldName = declaredElement.ShortName;
                
                RenameDataProvider provider = new RenameDataProvider(oldName, _newName);
                WorkflowProcessor p = new WorkflowProcessor(wf, solution);
                p.Initialize(new DataContext(null, declaredElement, textControl, solution, provider));
                p.ExecuteAction();
            }
        }

        private ModificationCookie ensureWritable()
        {
            OrderedHashSet<IProjectFile> set = new OrderedHashSet<IProjectFile>();
            IProjectFile projectFile = _declaration.GetContainingFile().ProjectFile;
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