using System;
using System.Web;
using System.Windows.Forms;
using AgentSmith.Comments;
using JetBrains.ActionManagement;
using JetBrains.Application;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Parsing;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.TextControl;
using JetBrains.Util;
using MessageBox=JetBrains.Util.MessageBox;

namespace AgentSmith.SmartPaste
{
    [ActionHandler(new[] { "AgentSmith.SmartPaste" })]
    internal class SmartInsertAction : IActionHandler
    {
        #region IActionHandler Members
        
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
                using (ReadLockCookie.Create())
                {
                    using (CommandCookie.Create("Smart Paste"))
                    {
                        ExecuteEx(context);
                    }
                }
            }
        }

        public void ExecuteEx(IDataContext context)
        {
            ITextControl editor = context.GetData(TextControlDataConstants.TEXT_CONTROL);
            Logger.Assert(editor != null, "Condition (editor != null) is false");
            if (editor == null)
                throw new ArgumentException("context");
            ISolution solution = context.GetData(JetBrains.IDE.DataConstants.SOLUTION);
            IDocument document = context.GetData(JetBrains.IDE.DataConstants.DOCUMENT);

            ICSharpFile file = PsiManager.GetInstance(solution).GetPsiFile(document) as ICSharpFile;
            if (file != null && editor != null)
            {
                using (ModificationCookie cookie = editor.Document.EnsureWritable())
                {
                    if (cookie.EnsureWritableResult == EnsureWritableResult.SUCCESS)
                    {
                        IElement element = file.FindElementAt(new TreeTextRange(new TreeOffset(editor.Caret.Offset())));
                        handleElement(editor, element, editor.Caret.Offset());
                    }
                }
            }
        }

        private static void handleElement(ITextControl editor, IElement element, int offset)
        {
            string stringToInsert = Clipboard.GetText();
            if (string.IsNullOrEmpty(stringToInsert))
            {
                return;
            }

            if (element is IDocCommentNode)
            {
                JetBrains.Util.dataStructures.TypedIntrinsics.Int32<DocLine> currentLineNumber =
                    editor.Document.GetCoordsByOffset(editor.Caret.Offset()).Line;
                string currentLine = editor.Document.GetLineText(currentLineNumber);
                int index = currentLine.IndexOf("///", StringComparison.Ordinal);
                if (index < 0)
                {
                    return;
                }
                string prefix = currentLine.Substring(0, index);

                if (shallEscape((IDocCommentNode)element, editor.Caret.Offset()) &&
                    HttpUtility.HtmlEncode(stringToInsert) != stringToInsert &&
                    MessageBox.ShowYesNo("Do you want the text to be escaped?"))
                {
                    stringToInsert = HttpUtility.HtmlEncode(stringToInsert);
                }

                stringToInsert = stringToInsert.Replace("\n", "\n" + prefix + "///");
            }

            if (element is ITokenNode)
            {
                ITokenNode token = (ITokenNode)element;
                if (token.GetTokenType() == CSharpTokenType.STRING_LITERAL &&
                    offset < token.GetTreeTextRange().EndOffset.Offset)
                {
                    string text = token.GetText();
                    if (text.StartsWith("@") && offset > token.GetTreeTextRange().StartOffset.Offset + 1)
                    {
                        stringToInsert = stringToInsert.Replace("\"", "\"\"");
                    }
                    else if (!text.StartsWith("@"))
                    {
                        stringToInsert = stringToInsert.
                            Replace("\\", "\\\\").
                            Replace("\a", "\\a").
                            Replace("\b", "\\b").
                            Replace("\f", "\\f").
                            Replace("\n", "\\n").
                            Replace("\r", "\\r").
                            Replace("\t", "\\t").
                            Replace("\v", "\\v").
                            Replace("\'", "\\'").
                            Replace("\"", "\\\"");
                    }
                }
            }

            editor.Document.InsertText(editor.Caret.Offset(), stringToInsert);
        }

        private static bool shallEscape(IDocCommentNode node, int offset)
        {
            IDocCommentBlockNode docBlock = node.GetContainingElement<IDocCommentBlockNode>(true);
            if (docBlock == null)
            {
                return false;
            }
            XmlDocLexer lexer = new XmlDocLexer(docBlock);
            lexer.Start();

            bool inCData = false;
            bool insideTag = false;
            while (lexer.TokenType != null)
            {
                if (lexer.TokenType == lexer.XmlTokenType.TAG_START)
                {
                    insideTag = true;
                }
                else if (lexer.TokenType == lexer.XmlTokenType.TAG_END)
                {
                    insideTag = false;
                }
                else if (lexer.TokenType == lexer.XmlTokenType.TAG_START1)
                {
                    insideTag = true;
                }
                else if (lexer.TokenType == lexer.XmlTokenType.TAG_END1)
                {
                    insideTag = false;
                }
                else if (lexer.TokenType == lexer.XmlTokenType.CDATA_START)
                {
                    inCData = true;
                }
                else if (lexer.TokenType == lexer.XmlTokenType.CDATA_END)
                {
                    inCData = false;
                }
                else if (offset >= lexer.TokenLocalRange.StartOffset && offset <= lexer.TokenLocalRange.EndOffset)
                {
                    return !inCData && !insideTag;
                }
                else if (offset < lexer.TokenLocalRange.StartOffset)
                {
                    return false;
                }
                lexer.Advance();
            }

            return false;
        }

        #endregion

        private static bool isAvailable(IDataContext context)
        {
            ISolution solution = context.GetData(JetBrains.IDE.DataConstants.SOLUTION);
            IDocument document = context.GetData(JetBrains.IDE.DataConstants.DOCUMENT);
            IProjectFile file = null;
            if (solution != null && document != null)
                file = document.GetProjectFile(solution);

            return solution != null && document != null && file != null && file.LanguageType == ProjectFileType.CSHARP;
        }
    }
}