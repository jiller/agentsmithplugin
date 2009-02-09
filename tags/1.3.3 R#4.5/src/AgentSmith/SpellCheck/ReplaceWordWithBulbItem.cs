using System;
using JetBrains.Application;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.TextControl;
using JetBrains.Util;

namespace AgentSmith.SpellCheck
{
    /// <summary>
    /// Bulb item which suggests to replace one word with another.
    /// Usually it suggests to replace unrecognized word with one from dictionary.
    /// </summary>
    public class ReplaceWordWithBulbItem : IBulbItem
    {
        private readonly string _option;
        private readonly DocumentRange _documentRange;

        /// <summary>
        /// Initializes new instance.
        /// </summary>
        /// <param name="range">Range in document that contains word to be replaced.</param>
        /// <param name="option">String to replace with.</param>
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