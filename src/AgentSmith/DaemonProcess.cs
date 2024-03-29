using System;
using System.Collections.Generic;
using AgentSmith.Comments;
using AgentSmith.Identifiers;
using AgentSmith.NamingConventions;
using AgentSmith.Options;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using AgentSmith.Strings;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Parsing;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Shell.Progress;

namespace AgentSmith
{
    public class DaemonProcess : ElementVisitor, IDaemonStageProcess, IRecursiveElementProcessor
    {
        private readonly IDeclarationAnalyzer[] _analyzers;

        private readonly GeneratedCodeRegionDetector _generatedCodeRegionDetector =
            new GeneratedCodeRegionDetector();

        private readonly SkipSpellCheckRegionDetector _skipSpellCheckRegionDetector =
            new SkipSpellCheckRegionDetector();

        private readonly List<HighlightingInfo> _highlightings = new List<HighlightingInfo>();
        private readonly IDaemonProcess _process;
        private readonly CodeStyleSettings _styleSettings;

        public DaemonProcess(IDaemonProcess daemonProcess)
        {
            _process = daemonProcess;
            _styleSettings = CodeStyleSettings.GetInstance(_process.Solution);
            //TODO:This might happen if plugin is activated manually
            if (_styleSettings == null)
            {
                _analyzers = new IDeclarationAnalyzer[0];
            }
            else
            {
                _analyzers = new IDeclarationAnalyzer[]
                    {
                        new NamingConventionsAnalyzer(_styleSettings.NamingConventionSettings, _process.Solution),
                        new CommentAnalyzer(_styleSettings.CommentsSettings, _process.Solution),
                        new IdentifierSpellCheckAnalyzer(_styleSettings.IdentifierDictionary, _process.Solution,
                                                         _styleSettings)
                    };
            }
        }

        #region IDaemonStageProcess Members

        public DaemonStageProcessResult Execute()
        {
            DaemonStageProcessResult result = new DaemonStageProcessResult();
            ICSharpFile file =
                (ICSharpFile) PsiManager.GetInstance(_process.Solution).GetPsiFile(_process.ProjectFile);
            ProcessFile(file);
            result.FullyRehighlighted = true;

            result.Highlightings = _highlightings.ToArray();
            return result;
        }

        #endregion

        #region IRecursiveElementProcessor Members

        public void ProcessBeforeInterior(IElement element)
        {
            _generatedCodeRegionDetector.Process(element);
            if (_generatedCodeRegionDetector.InGeneratedCode)
            {
                return;
            }

            _skipSpellCheckRegionDetector.Process(element);           

            if (!_skipSpellCheckRegionDetector.InSkipSpellCheck && 
                StringSpellCheckSuggestion.Enabled && 
                element is ITokenNode && 
                _styleSettings != null)
            {
                ITokenNode token = (ITokenNode) element;
                if (token.GetTokenType() == CSharpTokenType.STRING_LITERAL)
                {
                    string[] dicts = _styleSettings.StringsDictionary == null ? null : _styleSettings.StringsDictionary.Split(',');
                    ISpellChecker spellChecker = SpellCheckManager.GetSpellChecker(_process.Solution, dicts);
                    
                    IList<SuggestionBase> suggestions =
                        StringSpellChecker.SpellCheck(element.GetDocumentRange().Document, token, spellChecker,
                                                      _process.Solution);
                    foreach (SuggestionBase suggestion in suggestions)
                    {
                        addHighlighting(suggestion);
                    }
                }
            }

            IDeclaration declaration = element as IDeclaration;
            if (declaration == null)
            {
                return;
            }

            foreach (IDeclarationAnalyzer analyzer in _analyzers)
            {
                SuggestionBase[] result = analyzer.Analyze(declaration,
                    !_skipSpellCheckRegionDetector.InSkipSpellCheck);
                
                if (result != null)
                {
                    foreach (SuggestionBase highlighting in result)
                    {
                        addHighlighting(highlighting);
                    }
                }
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
            //TODO: Is it a hack?
            if (highlighting.Range.IsValid)
            {
                _highlightings.Add(new HighlightingInfo(highlighting.Range, highlighting));
            }
        }
    }
}