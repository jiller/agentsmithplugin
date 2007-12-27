using System;
using System.Windows.Forms;
using JetBrains.ActionManagement;
using JetBrains.ProjectModel;
using JetBrains.ReSharper;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Refactorings.Rename;
using JetBrains.ReSharper.TextControl;
using JetBrains.Shell;
using JetBrains.Shell.Progress;
using JetBrains.Util;

namespace AgentSmith.NamingConventions
{
    public class NamingConventionsBulbItem : IBulbItem
    {
        private class DataContext : DataContextBase
        {
            private readonly IReference _reference;
            private readonly IDeclaredElement _declaredElement;
            private readonly ITextControl _textControl;

            public DataContext(IReference reference, IDeclaredElement declaredElement, ITextControl textControl)
            {
                _reference = reference;
                _declaredElement = declaredElement;
                _textControl = textControl;
            }

            protected override object DoGetData(IDataConstant dataConstant)
            {
                if (dataConstant == DataConstants.REFERENCE)
                {
                    return _reference;
                }
                if (dataConstant == DataConstants.DECLARED_ELEMENT)
                {
                    return _declaredElement;
                }
                if (dataConstant == DataConstants.PSI_LANGUAGE_TYPE)
                {
                    return _declaredElement.Language;
                }
                if (dataConstant == DataConstants.TEXT_CONTROL)
                {
                    return _textControl;
                }
                return null;
            }
        }

        private readonly string _newName;
        private readonly IDeclaration _declaration;
        private readonly object _syncobj = new object();

        public NamingConventionsBulbItem(IDeclaration declaration, string newName)
        {
            _newName = newName;
            _declaration = declaration;
        }

        #region IBulbItem Members

        public void Execute(ISolution solution, ITextControl textControl)
        {
            lock (_syncobj)
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
                                RenameRefactoringWorkflow wf = new RenameRefactoringWorkflow();
                                if (wf.Initialize(new DataContext(null, _declaration.DeclaredElement, textControl), null))
                                {
                                    wf.SetName(_newName, NullProgressIndicator.INSTANCE, false);
                                    if (wf.ConflictSearcher.SearchConflicts(NullProgressIndicator.INSTANCE).Conflicts.Count > 0)
                                    {
                                        MessageBox.Show("Conflicts were found. Can not rename.");
                                    }
                                    else
                                    {
                                        PsiManager manager = PsiManager.GetInstance(solution);
                                        manager.DoTransaction(delegate { wf.Execute(NullProgressIndicator.INSTANCE); });
                                    }
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

        public void ExecuteEx(ISolution solution, ITextControl textControl)
        {
            RenameRefactoringWorkflow wf = new RenameRefactoringWorkflow();
            if (wf.Initialize(new DataContext(null, _declaration.DeclaredElement, textControl), null))
            {
                wf.SetName(_newName, NullProgressIndicator.INSTANCE, false);
                if (wf.ConflictSearcher.SearchConflicts(NullProgressIndicator.INSTANCE).Conflicts.Count > 0)
                {
                    MessageBox.Show("Conflicts were found. Can not rename.");
                }
                else
                {
                    wf.Execute(NullProgressIndicator.INSTANCE);
                }
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