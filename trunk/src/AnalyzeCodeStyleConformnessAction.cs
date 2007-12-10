using System;
using System.Collections.Generic;
using JetBrains.ReSharper;
using JetBrains.ReSharper.ActionManagement;
using JetBrains.ReSharper.CodeInsight;
using JetBrains.ReSharper.CodeView.Occurences;
using JetBrains.ReSharper.CodeView.Util;
using JetBrains.ReSharper.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Shell.Progress;

namespace AgentSmith
{
    [ActionHandler("AnalyzeCodeStyleConformness")]
    internal class AnalyzeCodeStyleConformnessAction : IActionHandler
    {
        public bool Update(IDataContext context, ActionPresentation presentation, DelegateUpdate nextUpdate)
        {
            ActionScope scope1 = new ActionFilesCollector.Context(context).GetActionScope();
            if (scope1 != ActionScope.NONE)
            {
                return (scope1 != ActionScope.SELECTION);
            }
            return false;
        }

        public void Execute(IDataContext context, DelegateExecute nextExecute)
        {
            ISolution solution1 = context.GetData(DataConstants.SOLUTION);
            PsiManager.GetInstance(solution1).CommitAllDocuments();
            if (PsiManager.GetInstance(solution1).WaitForCaches())
            {
                ActionFilesCollector.Context context1 = new ActionFilesCollector.Context(context);

                using (ProgressWindow window1 = new ProgressWindow(true))
                {
                    bool canceled;
                    FindResultsProvider prov =
                        (FindResultsProvider)
                        window1.ExecuteTask(new TaskWithProgress(new CodeAnalyzerWorker(context1).Run),
                                            "Agent Smith is looking for code style violations...", out canceled);
                    if (prov != null)
                    {
                        if (prov.NamingConventions.GetOccurences().Count > 0)
                        {
                            CodeViewUtil.ShowResults(
                                new MyDescriptor(solution1, prov.NamingConventions.GetOccurences(),
                                                 "Naming convention violations."));
                        }
                        if (prov.Exceptions.GetOccurences().Count > 0)
                        {
                            CodeViewUtil.ShowResults(
                                new MyDescriptor(solution1, prov.Exceptions.GetOccurences(), "Catch Or Specify violations."));
                        }
                        if (prov.Comments.GetOccurences().Count > 0)
                        {
                            CodeViewUtil.ShowResults(
                                new MyDescriptor(solution1, prov.Comments.GetOccurences(), "Comments violations."));
                        }
                        if (prov.Structure.GetOccurences().Count > 0)
                        {
                            CodeViewUtil.ShowResults(
                                new MyDescriptor(solution1, prov.Structure.GetOccurences(), "Member order violations"));
                        }
                    }
                }
            }
        }

        private class MyDescriptor : OccurenceBrowserDescriptor
        {
            private string _name;
            public MyDescriptor(ISolution solution, ICollection<IOccurence> occurences, string name) : base(solution)
            {
                _name = name;
                SetResults(occurences);
            }

            public override string Title
            {
                get { return _name; }
            }
        }
    }
}