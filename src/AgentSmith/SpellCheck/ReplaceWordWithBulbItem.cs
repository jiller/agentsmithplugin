using System;
using JetBrains.Application;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.TextControl;
using JetBrains.Util;

namespace AgentSmith.SpellCheck
{
    public class ReplaceWordWithBulbItem : IBulbItem
    {
        private readonly string _option;
        private readonly DocumentRange _documentRange;

        public ReplaceWordWithBulbItem(DocumentRange range, string option)
        {
            _option = option;
            _documentRange = range;
        }

        #region IBulbItem Members

        public void Execute(ISolution solution, ITextControl textControl)
        {
            using (CommandCookie.Create(Text))
            {
                using (ModificationCookie ensureWritable = _documentRange.Document.EnsureWritable())
                {
                    if (ensureWritable.EnsureWritableResult == EnsureWritableResult.SUCCESS)
                    {
                        textControl.Document.ReplaceText(_documentRange.TextRange, _option);
                    }
                }
            }
        }

        public string Text
        {
            get { return String.Format("Replace with '{0}'.", _option); }
        }

        #endregion
    }
}