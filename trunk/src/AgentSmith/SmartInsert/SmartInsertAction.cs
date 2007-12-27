using System;
using JetBrains.ActionManagement;
using JetBrains.ProjectModel;
using JetBrains.ReSharper;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.TextControl;
using JetBrains.Shell;
using JetBrains.Util;

namespace AgentSmith.SmartInsert
{
    [ActionHandler("SmartInsert")]
    internal class AnalyzeCodeStyleConformnessAction : IActionHandler
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
                IProjectFile file = (IProjectFile)context.GetData(DataConstants.PROJECT_MODEL_ELEMENT);
                PsiLanguageType languageType = ProjectFileLanguageServiceManager.Instance.GetPsiLanguageType(file);
                //ILineCommentActionProvider commentActionProvider = CodeInsightServiceManager.Instance[languageType].LineCommentActionProvider;
                ITextControl editor = context.GetData(DataConstants.TEXT_CONTROL);
                Logger.Assert(editor != null, "Condition (editor != null) is false");
                ISolution solution = context.GetData(DataConstants.SOLUTION);
                CachingLexer lexer = CachingLexerService.GetInstance(solution).GetCachingLexer(editor);
                Logger.Assert(lexer != null, "No caching lexer for file type {0}, language type {1}", new object[] { file.LanguageType, languageType });

                PerformLineComment(editor, lexer);
            }
        }

        #endregion

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
            if (context.GetData(DataConstants.TEXT_CONTROL) == null)
            {
                return false;
            }
            IProjectFile file = context.GetData(DataConstants.PROJECT_MODEL_ELEMENT) as IProjectFile;
            if (file == null)
            {
                return false;
            }
            PsiLanguageType languageType = ProjectFileLanguageServiceManager.Instance.GetPsiLanguageType(file);
            if (LanguageServiceManager.Instance.GetLanguageService(languageType) == null)
            {
                return false;
            }
            return languageType.Name == "CSharp";
            //ICodeInsightService codeInsightService = CodeInsightServiceManager.Instance[languageType];
            //return ((codeInsightService != null) && (codeInsightService.LineCommentActionProvider != null));
        }
    }
}