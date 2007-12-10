using System;
using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.CodeInsight;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;

using JetBrains.Shell;
using JetBrains.Shell.Progress;

namespace AgentSmith
{
    internal class CodeAnalyzerWorker
    {
        public CodeAnalyzerWorker(ActionFilesCollector.Context context)
        {
            _context = context;
        }

        public object Run(IProgressIndicator pi)
        {
            FindResultsProvider prov = new FindResultsProvider();

            
            IList<IProjectFile> files = ActionFilesCollector.GetFiles(_context);

            pi.Start(files.Count);
            using (FileProcessor fileProcessor = new FileProcessor(prov, _context.Solution))
            {
                using (ReadLockCookie.Create())
                {
                    foreach (IProjectFile file in files)
                    {
                        PsiManager manager = PsiManager.GetInstance(_context.Solution);
                        ICSharpFile csFile = manager.GetPsiFile(file) as ICSharpFile;
                        pi.CurrentItemText = file.Name;
                        fileProcessor.ProcessFile(csFile);
                        pi.Advance(1);
                    }
                }             
            }
            pi.Stop();

            
            return prov;
        }

        private ActionFilesCollector.Context _context;        
    }
}