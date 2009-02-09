using System;
using JetBrains.ActionManagement;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Services;
using JetBrains.ReSharper.Refactorings.Rename;
using JetBrains.TextControl;

namespace AgentSmith.NamingConventions
{
    internal class DataContext : DataContextBase
    {
        private readonly IReference _reference;
        private readonly IDeclaredElement _declaredElement;
        private readonly ITextControl _textControl;
        private readonly ISolution _solution;
        private readonly IRenameDataProvider _renameDataProvider;

        public DataContext(IReference reference, IDeclaredElement declaredElement, ITextControl textControl,
                           ISolution solution, IRenameDataProvider renameDataProvider)
        {
            _reference = reference;
            _declaredElement = declaredElement;
            _textControl = textControl;
            _solution = solution;
            _renameDataProvider = renameDataProvider;
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
            if (dataConstant == TextControlDataConstants.TEXT_CONTROL)
            {
                return _textControl;
            }
            if (dataConstant == JetBrains.IDE.DataConstants.SOLUTION)
            {
                return _solution;
            }
            if (dataConstant == RenameWorkflow.RENAME_DATA_PROVIDER)
            {
                return _renameDataProvider;
            }            

            return null;
        }


        protected override object DoSetData(IDataConstant constant, object data)
        {
            throw new NotImplementedException();
        }
    }
}