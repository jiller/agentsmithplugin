using System;
using System.Collections.Generic;
using System.Resources;
using AgentSmith.Options;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace AgentSmith.ResX
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
            DaemonStageProcessResult result = new DaemonStageProcessResult();
            CodeStyleSettings styleSettings = CodeStyleSettings.GetInstance(_file.GetSolution());
            if (styleSettings == null)
            {
                //TODO:This might happen if plugin is activated manually
                return result;
            }

            List<HighlightingInfo> highlightings = new List<HighlightingInfo>();
            IModuleAttributes moduleAttributes = PsiManager.GetInstance(_file.GetSolution()).GetModuleAttributes(_file.GetProject());
            string defaultResXDic = "en-US";
            IList<IAttributeInstance> attributes = moduleAttributes
                .GetAttributeInstances(new CLRTypeName(typeof (NeutralResourcesLanguageAttribute).FullName));
            if (attributes != null &&
                attributes.Count > 0 &&
                attributes[0].PositionParameter(0).Value != null)
            {
                defaultResXDic = attributes[0].PositionParameter(0).Value.ToString();
            }

            ISpellChecker checker = SpellCheckManager.GetSpellChecker(_file, defaultResXDic);
            if (checker != null)
            {
                foreach (IXmlTokenNode token in getStringsToCheck())
                {
                    WordLexer lexer = new WordLexer(token.GetText());
                    lexer.Start();
                    while (lexer.TokenType != null)
                    {
                        if (SpellCheckUtil.ShouldSpellCheck(lexer.TokenText) &&
                            !checker.TestWord(lexer.TokenText, false))
                        {
                            DocumentRange docRange = token.GetDocumentRange();
                            TextRange textRange = new TextRange(docRange.TextRange.StartOffset + lexer.TokenStart,
                                                                docRange.TextRange.StartOffset + lexer.TokenEnd);
                            DocumentRange range = new DocumentRange(docRange.Document, textRange);
                            
                            ResXSpellHighlighting highlighting =
                                new ResXSpellHighlighting(lexer.TokenText, _file, checker, range);
                            
                            highlightings.Add(new HighlightingInfo(range, highlighting));
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