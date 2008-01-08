using System;
using System.Windows.Forms;
using System.Xml;
using JetBrains.ActionManagement;
using JetBrains.ProjectModel;
using JetBrains.ReSharper;
using JetBrains.ReSharper.ClipboardManager;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.TextControl;
using JetBrains.Shell;
using JetBrains.Util;

namespace AgentSmith.SmartInsert
{
    [ActionHandler(new string[] {"AgentSmith.SmartPaste"})]
    internal class SmartInsertAction : IActionHandler
    {
        #region IActionHandler Members

        /// <summary>
        /// ITextControl editor = context.GetData(DataConstants.TEXT_CONTROL);
        /// Logger.Assert(editor != null, "Condition (editor != null) is false");
        /// ISolution solution = context.GetData(DataConstants.SOLUTION);
        /// </summary>
        ///  using (CommandCookie.Create("Smart Paste"))
        ///                    {
        ///                        PsiManager manager = PsiManager.GetInstance(solution);
        ///                        manager.DoTransaction(delegate { ExecuteEx(context); });
        ///                    }       
        /// <param name="context"></param>
        /// <param name="presentation"></param>
        /// <param name="nextUpdate"></param>
        /// <returns></returns>        
        public bool Update(IDataContext context, ActionPresentation presentation, DelegateUpdate nextUpdate)
        {
            if (!isAvailable(context))
            {
                return nextUpdate();
            }
            return true;
        }

        public void Execute(IDataContext context, DelegateExecute nextExecute)
        {
            if (!isAvailable(context))
            {
                nextExecute();
            }
            else
            {
                ISolution solution = context.GetData(DataConstants.SOLUTION);
                PsiManager psiManager = PsiManager.GetInstance(solution);
                if (psiManager.WaitForCaches())
                {
                    using (CommandCookie.Create("Smart Paste"))
                    {
                        PsiManager manager = PsiManager.GetInstance(solution);
                        manager.DoTransaction(delegate { ExecuteEx(context); });
                    }
                }
            }
        }

        public void ExecuteEx(IDataContext context)
        {
            ITextControl editor = context.GetData(DataConstants.TEXT_CONTROL);
            Logger.Assert(editor != null, "Condition (editor != null) is false");
            ISolution solution = context.GetData(DataConstants.SOLUTION);
            IDocument document = context.GetData(DataConstants.DOCUMENT);

            ICSharpFile file = PsiManager.GetInstance(solution).GetPsiFile(document) as ICSharpFile;
            if (file != null && editor != null)
            {
                using (ModificationCookie cookie = editor.Document.EnsureWritable())
                {
                    if (cookie.EnsureWritableResult == EnsureWritableResult.SUCCESS)
                    {
                        //using (GlobalIntelliSenseSettings.Instance.CreateDisableAutoFormatCookie())
                        {
                            IElement element = file.FindElementAt(new TextRange(editor.CaretModel.Offset));
                            HandleElement(editor, element);
                        }
                    }
                }
            }
        }

        private void HandleElement(ITextControl editor, IElement element)
        {
            if (element is IDocCommentNode)
            {
                int currentLineNumber = editor.Document.GetCoordsByOffset(editor.CaretModel.Offset).Line;
                string currentLine = editor.Document.GetLine(currentLineNumber);
                int index = currentLine.IndexOf("///");
                if (index < 0)
                {
                    return;
                }
                string prefix = currentLine.Substring(0, index);
                string clipboard = ClipboardManager.Instance.ClipboardEntries.RecentItem;
                if (clipboard != null)
                {
                    string stringToInsert = clipboard.Replace("\n", "\n" + prefix + "///");                    
                    editor.Document.InsertText(editor.CaretModel.Offset, stringToInsert);
                }
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="lexer"></param>
        public static void PerformLineComment(ITextControl editor, CachingLexer lexer)
        {
            using (ModificationCookie cookie = editor.Document.EnsureWritable())
            {
                if (cookie.EnsureWritableResult == EnsureWritableResult.SUCCESS)
                {
                    //using (IDisposable anotherCookie = GlobalIntelliSenseSettings.Instance.CreateDisableAutoFormatCookie())
                    {
                        CommandProcessor commandProcessor = CommandProcessor.Instance;
                        try
                        {
                            commandProcessor.BeginCommand("Smart insert");
                            //editor.Document.DeleteText(new TextRange(commentRange.EndOffset - endCommentLen, commentRange.EndOffset));                            
                        }
                        finally
                        {
                            commandProcessor.EndCommand();
                        }
                    }
                }
            }
        }

        private static bool isAvailable(IDataContext context)
        {
            ISolution solution = context.GetData(DataConstants.SOLUTION);
            if (solution == null)
            {
                MessageBox.Show("solution is null");
                return false;
            }

            IDocument document = context.GetData(DataConstants.DOCUMENT);
            if (document == null)
            {
                MessageBox.Show("document is null");
                return false;
            }

            ICSharpFile csfile = PsiManager.GetInstance(solution).GetPsiFile(document) as ICSharpFile;
            if (csfile == null)
            {
                MessageBox.Show("csfile is null");
                return false;
            }
            return csfile != null;
        }
    }
}