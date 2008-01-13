using System;
using System.Collections.Generic;
using AgentSmith.Comments;
using AgentSmith.NamingConventions;
using AgentSmith.Options;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Parsing;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Shell.Progress;
using JetBrains.Util;

namespace AgentSmith
{
    public class DaemonProcess : ElementVisitor, IDaemonStageProcess, IRecursiveElementProcessor
    {
        private readonly IDeclarationAnalyzer[] _analyzers;
        private readonly IDaemonProcess _process;
        private readonly List<HighlightingInfo> _highlightings = new List<HighlightingInfo>();

        public DaemonProcess(IDaemonProcess daemonProcess)
        {
            _process = daemonProcess;
            CodeStyleSettings styleSettings = CodeStyleSettings.GetInstance(_process.Solution);
            _analyzers = new IDeclarationAnalyzer[]
                {
                    new NamingConventionsAnalyzer(styleSettings.NamingConventionSettings, _process.Solution),
                    new CommentAnalyzer(styleSettings.CommentsSettings, _process.Solution),
                };
        }

        #region IDaemonStageProcess Members

        public DaemonStageProcessResult Execute()
        {
            DaemonStageProcessResult result = new DaemonStageProcessResult();
            ICSharpFile myFile = (ICSharpFile)PsiManager.GetInstance(_process.Solution).GetPsiFile(_process.ProjectFile);
            ProcessFile(myFile);
            result.FullyRehighlighted = true;

            result.Highlightings = _highlightings.ToArray();
            return result;
        }

        #endregion

        #region IRecursiveElementProcessor Members

        public void ProcessBeforeInterior(IElement element)
        {
            if (element is ITokenNode)
            {
                ITokenNode token = (ITokenNode)element;
                if (token.GetTokenType() == CSharpTokenType.STRING_LITERAL)
                {                    
                    ISpellChecker spellChecker = SpellCheckManager.GetSpellChecker(_process.Solution);

                    spellCheck(element.GetDocumentRange().Document, token, spellChecker);
                }
            }

            IDeclaration declaration = element as IDeclaration;
            if (declaration == null)
            {
                return;
            }

            foreach (IDeclarationAnalyzer analyzer in _analyzers)
            {
                foreach (SuggestionBase highlighting in analyzer.Analyze(declaration))
                {
                    addHighlighting(highlighting);
                }
            }
        }      
        
        private string unescape(string text)
        {
            if (!text.StartsWith("@"))
            {
                return text.Replace("\\a", "  ").
                    Replace("\\b", "  ").
                    Replace("\\f", "  ").
                    Replace("\\n", "  ").
                    Replace("\\r", "  ").
                    Replace("\\t", "  ").
                    Replace("\\v", "  ");
            }
            return text;
        }

        private void spellCheck(IDocument document, ITokenNode token, ISpellChecker spellChecker)
        {
            ILexer wordLexer = new WordLexer(unescape(token.GetText()));
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
                        addHighlighting(new StringSpellCheckSuggestion(documentRange));                        
                    }
                }

                wordLexer.Advance();
            }            
        }

        public bool InteriorShouldBeProcessed(IElement element)
        {
            return true;
        }

        public void ProcessAfterInterior(IElement element)
        {
        }

        public bool ProcessingIsFinished
        {
            get
            {
                if (_process.InterruptFlag)
                {
                    throw new ProcessCancelledException();
                }
                return false;
            }
        }

        #endregion

        public void ProcessFile(ICSharpFile file)
        {
            file.ProcessDescendants(this);
        }

        private void addHighlighting(SuggestionBase highlighting)
        {
            _highlightings.Add(new HighlightingInfo(highlighting.Range, highlighting));
        }
    }
}