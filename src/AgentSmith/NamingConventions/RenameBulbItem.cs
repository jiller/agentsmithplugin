using System;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Refactorings.Rename;
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
                DataContext context = new DataContext(null, _declaration.DeclaredElement, textControl,
                    solution, null);
                RenameWorkflow.ExcuteRename(context);
            }
        }

        public string Text
        {
            get { return "Rename..."; }
        }

        #endregion       
    }
}