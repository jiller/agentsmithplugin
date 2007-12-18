using System;
using System.Collections.Generic;
using AgentSmith.Comments;
using AgentSmith.Comments.NetSpell;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp.Stages;
using JetBrains.ReSharper.Daemon.Impl;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace AgentSmith
{
    [DaemonStage(StagesBefore = new Type[] {typeof (GlobalErrorStage)},
        StagesAfter = new Type[] {typeof (LanguageSpecificDaemonStage)}, RunForInvisibleDocument = true)]
    public class ResXDaemonStage : IDaemonStage
    {
        public IDaemonStageProcess CreateProcess(IDaemonProcess process)
        {
            if (process.ProjectFile.Name.EndsWith(".resx"))
            {
                return new ResXProcess(process.ProjectFile, this);
            }

            return null;
        }

        public ErrorStripeRequest NeedsErrorStripe(IProjectFile projectFile)
        {
            return ErrorStripeRequest.STRIPE_AND_ERRORS;
        }
    }

    internal class ResXProcess : DaemonProcessBase, IDaemonStageProcess
    {
        private readonly IProjectFile _file;

        public ResXProcess(IProjectFile file, ResXDaemonStage stage)
            : base(file, new IDaemonStage[] {stage})
        {
            _file = file;
        }

        public override bool IsRangeInvalidated(TextRange range)
        {
            throw new NotImplementedException();
        }

        public override bool HasInvalidatedRangeOutside(ICollection<TextRange> ranges)
        {
            throw new NotImplementedException();
        }

        public override void CommitHighlighters(CommitContext context)
        {
            throw new NotImplementedException();
        }

        public override bool FullRehighlightingRequired
        {
            get { throw new NotImplementedException(); }
        }

        public DaemonStageProcessResult Execute()
        {
            DaemonStageProcessResult result = new DaemonStageProcessResult();
            List<HighlightingInfo> highlightings = new List<HighlightingInfo>();
            ISpellChecker checker = SpellChecker.GetInstance(_file.GetSolution());
            foreach (IXmlTokenNode token in GetStringsToCheck())
            {
                WordLexer lexer = new WordLexer(token.GetText());
                lexer.Start();
                while (lexer.TokenType != null)
                {
                    if (!checker.TestWord(lexer.TokenText))
                    {
                        string text = String.Format("Word {0} is not in dictionary.", lexer.TokenText);
                        
                        DocumentRange docRange = token.GetDocumentRange();
                        DocumentRange range = new DocumentRange(docRange.Document, new TextRange(docRange.TextRange.StartOffset + lexer.TokenStart, docRange.TextRange.StartOffset + lexer.TokenEnd));                        
                        highlightings.Add(new HighlightingInfo(range,
                                                               new ResxSpellHighlighting(token.GetDocumentRange(), token, text)));
                    }
                    lexer.Advance();
                }                
            }
            result.Highlightings = highlightings.ToArray();
            result.FullyRehighlighted = result.Highlightings.Length > 0;
            return result;
        }

        private IList<IXmlTokenNode> GetStringsToCheck()
        {
            IList<IXmlTokenNode> tokens = new List<IXmlTokenNode>();
            IXmlFile xmlFile = PsiManager.GetInstance(_file.GetSolution()).GetPsiFile(_file) as IXmlFile;
            if (xmlFile != null)
            {
                IXmlTag root = xmlFile.GetTag(delegate(IXmlTag tag) { return tag.TagName == "root"; });

                if (root != null)
                {
                    IList<IXmlTag> datas = root.GetTags(delegate(IXmlTag tag) { return tag.TagName == "data"; });
                    foreach (IXmlTag data in datas)
                    {
                        if (data.GetAttribute("type") == null)
                        {
                            IXmlTag val = data.GetTag(delegate(IXmlTag tag) { return tag.TagName == "value"; });
                            if (val != null)
                            {
                                IXmlTagNode node = val.ToTreeNode();
                                if (node.FirstChild != null && node.FirstChild.NextSibling != null)
                                {
                                    IXmlTokenNode value = node.FirstChild.NextSibling as IXmlTokenNode;
                                    if (value != null)
                                    {
                                        tokens.Add(value);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return tokens;
        }

        [ConfigurableSeverityHighlighting(ResxSpellHighlighting.NAME)]
        internal class ResxSpellHighlighting : SuggestionBase
        {
            public const string NAME = "ResxSpellCheckSuggestion";
            private readonly DocumentRange _range;

            public ResxSpellHighlighting(DocumentRange range, IElement element, string toolTip)
                : base(element, toolTip)
            {
                _range = range;
            }

            public override Severity Severity
            {
                get { return HighlightingSettingsManager.Instance.Settings.GetSeverity(NAME); }
            }

            public override DocumentRange Range
            {
                get { return _range; }
            }
        }
    }
}