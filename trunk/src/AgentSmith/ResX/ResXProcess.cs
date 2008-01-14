using System;
using System.Collections.Generic;
using AgentSmith.Options;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace AgentSmith.Resx
{
    internal class ResXProcess : IDaemonStageProcess
    {
        private readonly IProjectFile _file;

        public ResXProcess(IProjectFile file)
        {
            _file = file;
        }

        #region IDaemonStageProcess Members

        public DaemonStageProcessResult Execute()
        {
            CodeStyleSettings styleSettings = CodeStyleSettings.GetInstance(_file.GetSolution());

            DaemonStageProcessResult result = new DaemonStageProcessResult();
            List<HighlightingInfo> highlightings = new List<HighlightingInfo>();
            ISpellChecker checker = SpellCheckManager.GetSpellChecker(_file);
            if (checker != null)
            {
                foreach (IXmlTokenNode token in getStringsToCheck())
                {
                    WordLexer lexer = new WordLexer(token.GetText());
                    lexer.Start();
                    while (lexer.TokenType != null)
                    {
                        if (SpellCheckUtil.ShouldSpellCheck(lexer.TokenText) && !checker.TestWord(lexer.TokenText, false))
                        {
                            DocumentRange docRange = token.GetDocumentRange();
                            DocumentRange range = new DocumentRange(docRange.Document, new TextRange(docRange.TextRange.StartOffset + lexer.TokenStart, docRange.TextRange.StartOffset + lexer.TokenEnd));
                            highlightings.Add(new HighlightingInfo(range,
                                new ResXSpellHighlighting(lexer.TokenText, _file, styleSettings.CommentsSettings, range, token)));
                        }
                        lexer.Advance();
                    }
                }
            }
            result.Highlightings = highlightings.ToArray();
            result.FullyRehighlighted = result.Highlightings.Length > 0;
            return result;
        }

        #endregion

        private IList<IXmlTokenNode> getStringsToCheck()
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
    }
}