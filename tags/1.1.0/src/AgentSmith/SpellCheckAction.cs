using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.ActionManagement;
using JetBrains.ProjectModel;
using JetBrains.ReSharper;
using JetBrains.ReSharper.CodeView.Highlighting;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Parsing;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.TextControl;
using JetBrains.Util;

namespace AgentSmith
{
    [ActionHandler(new string[] {"AgentSmith.SpellCheck"})]
    public class SpellCheckAction : IActionHandler
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
            ISolution solution = context.GetData(DataConstants.SOLUTION);
            IDocument document = context.GetData(DataConstants.DOCUMENT);
            ITextControl editor = context.GetData(DataConstants.TEXT_CONTROL);
            ITextControl textControl = context.GetData(DataConstants.TEXT_CONTROL);
            ICSharpFile file = PsiManager.GetInstance(solution).GetPsiFile(document) as ICSharpFile;
            if (file == null)
            {
                return;
            }

            ISpellChecker spellChecker = SpellCheckManager.GetSpellChecker(solution);
            
            IElement element = file.FindElementAt(new TextRange(editor.CaretModel.Offset));
            if (element is ITokenNode)
            {
                ITokenNode token = (ITokenNode) element;
                if (token.GetTokenType() == CSharpTokenType.STRING_LITERAL)
                {
                    List<CustomHighlightingEntry> entries = spellCheck(document, token, spellChecker);

                    if (entries.Count == 0)
                    {
                        MessageBox.Show("No spell check errors.");
                    }
                    else
                    {
                        CustomHighlightingRequest request = new CustomHighlightingRequest(textControl, entries);
                        CustomHighlightingManager.GetInstance(solution).Show(request);
                    }
                }
            }            
        }

        private List<CustomHighlightingEntry> spellCheck(IDocument document, ITokenNode token, ISpellChecker spellChecker)
        {
            List<CustomHighlightingEntry> entries = new List<CustomHighlightingEntry>();

            ILexer wordLexer = new WordLexer(token.GetText());
            wordLexer.Start();
            while (wordLexer.TokenType != null)
            {
                if (SpellCheckUtil.ShouldSpellCheck(wordLexer.TokenText))
                {
                    if (spellChecker != null && !spellChecker.TestWord(wordLexer.TokenText, false))
                    {
                        int start = token.GetTreeStartOffset() + wordLexer.TokenStart;
                        int end = start + wordLexer.TokenText.Length;

                        DocumentRange documentRange = new DocumentRange(document, new TextRange(start, end));
                        CustomHighlightingEntry entry = new CustomHighlightingEntry(documentRange,
                                                        CustomHighlightingKind.Other);
                        entries.Add(entry);
                    }
                }
                        
                wordLexer.Advance();
            }
            return entries;
        }

        #endregion

        private static bool isAvailable(IDataContext context)
        {
            ISolution solution = context.GetData(DataConstants.SOLUTION);
            IDocument document = context.GetData(DataConstants.DOCUMENT);
            PsiLanguageType languageType = context.GetData(DataConstants.PSI_LANGUAGE_TYPE);

            return solution != null && document != null && languageType != null && languageType.Name == "CSHARP";
        }
    }
}