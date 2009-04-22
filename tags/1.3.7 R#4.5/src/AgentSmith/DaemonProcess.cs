using System;
using System.Collections.Generic;
using AgentSmith.Comments;
using AgentSmith.Identifiers;
using AgentSmith.NamingConventions;
using AgentSmith.Options;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using AgentSmith.Strings;
using JetBrains.Application.Progress;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Parsing;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith
{
    /// <summary>
    /// Main Agent Smith process.
    /// Performs spell checking of everything in C# files and checks naming conventions.
    /// </summary>
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
                                     new NamingConventionsAnalyzer(_styleSettings.NamingConventionSettings,
                                                                   _process.Solution),
                                     new CommentAnalyzer(_styleSettings.CommentsSettings, _process.Solution,
                                                         _styleSettings.CompiledPatternsToIgnore),
                                     new IdentifierSpellCheckAnalyzer(_styleSettings.IdentifierDictionary,
                                                                      _process.Solution,
                                                         _styleSettings)
                    };
            }
        }

        #region IDaemonStageProcess Members

        public void Execute(Action<DaemonStageResult> action)
        {            
            ICSharpFile file =
                (ICSharpFile) PsiManager.GetInstance(_process.Solution).GetPsiFile(_process.ProjectFile);
            ProcessFile(file);
            DaemonStageResult result = new DaemonStageResult(_highlightings.ToArray());
            action(result);
        }

        #endregion

        #region IRecursiveElementProcessor Members

        public void ProcessBeforeInterior(IElement element)
        {
            //Don't process generated regions at all.
            _generatedCodeRegionDetector.Process(element);
            if (_generatedCodeRegionDetector.InGeneratedCode)
            {
                return;
            }

            //Don't spell check regions inside '//agentsmith spell check enable/disable' regions.
            _skipSpellCheckRegionDetector.Process(element);           

            //If this element is string then spell check it.
            testIfStringAndSpellCheck(element);

            IDeclaration declaration = element as IDeclaration;
            if (declaration == null)
            {
                return;
            }

            //Analyze naming conventions, perform identifier spell checking etc.
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

        private void testIfStringAndSpellCheck(IElement element)
        {
            if (_skipSpellCheckRegionDetector.InSkipSpellCheck ||               
                !(element is ITokenNode) ||
                _styleSettings == null)
            {
                return;
            }

            ITokenNode token = (ITokenNode) element;

            if (!VerbatimStringSpellCheckSuggestion.Enabled && token.GetText().StartsWith("@"))
                return;

            if (!StringSpellCheckSuggestion.Enabled && !token.GetText().StartsWith("@"))
                return;

            if (token.GetTokenType() == CSharpTokenType.STRING_LITERAL)
            {
                //load spell checker that can handle multiple languages configured for strings.
                string[] dicts = _styleSettings.StringsDictionary == null
                                     ? null
                                     : _styleSettings.StringsDictionary.Split(',');
                ISpellChecker spellChecker = SpellCheckManager.GetSpellChecker(_process.Solution, dicts);

                //Spell check string and get suggestions.
                IList<SuggestionBase> suggestions =
                    StringSpellChecker.SpellCheck(element.GetDocumentRange().Document, token, spellChecker,
                                                  _process.Solution, _styleSettings.CompiledPatternsToIgnore);
                foreach (SuggestionBase suggestion in suggestions)
                {
                    addHighlighting(suggestion);
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
            if (highlighting.Range.IsValid())
            {
                _highlightings.Add(new HighlightingInfo(highlighting.Range, highlighting));
            }
        }
    }
}