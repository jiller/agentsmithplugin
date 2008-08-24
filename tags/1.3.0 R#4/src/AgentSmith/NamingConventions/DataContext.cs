using System;
using JetBrains.ActionManagement;
using JetBrains.ReSharper;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.TextControl;

namespace AgentSmith.NamingConventions
{
    internal class DataContext : DataContextBase
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
}