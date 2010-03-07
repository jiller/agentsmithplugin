using System;
using System.Collections.Generic;
using AgentSmith.Options;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Impl.Caches2;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;
using System.Resources;

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

        public void Execute(Action<DaemonStageResult> action)
        {            
            CodeStyleSettings styleSettings = CodeStyleSettings.GetInstance(_file.GetSolution());
            if (styleSettings == null)
            {
                //TODO:This might happen if plugin is activated manually
                //return result;
                return;
            }

            List<HighlightingInfo> highlightings = new List<HighlightingInfo>();            
            IFile psiFile = PsiManager.GetInstance(_file.GetSolution()).GetPsiFile(_file);
            if (psiFile == null)
                return;
            IPsiModule module = psiFile.GetPsiModule();
            IModuleAttributes moduleAttributes = CacheManagerEx.GetInstance(_file.GetSolution()).GetModuleAttributes(module);
            string defaultResXDic = "en-US";
            IList<IAttributeInstance> attributes = moduleAttributes
                .GetAttributeInstances(new CLRTypeName(typeof (NeutralResourcesLanguageAttribute).FullName));
            if (attributes != null &&
                attributes.Count > 0 &&
                attributes[0].PositionParameter(0).ConstantValue.Value != null)
            {
                defaultResXDic = attributes[0].PositionParameter(0).ConstantValue.Value.ToString();
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
            action(new DaemonStageResult(highlightings.ToArray()));            
        }

        #endregion

        private IList<IXmlTokenNode> getStringsToCheck()
        {
            IList<IXmlTokenNode> tokens = new List<IXmlTokenNode>();
            IXmlFile xmlFile = PsiManager.GetInstance(_file.GetSolution()).GetPsiFile(_file) as IXmlFile;
            if (xmlFile != null)
            {
                IXmlTag root = xmlFile.GetTag(delegate(IXmlTag tag) { return tag.GetTagName() == "root"; });

                if (root != null)
                {
                    IEnumerable<IXmlTag> datas = root.GetTags<IXmlTag>(delegate(IXmlTag tag) { return tag.GetTagName() == "data"; });
                    foreach (IXmlTag data in datas)
                    {
                        if (data.GetAttribute("type") == null)
                        {
                            IXmlTag val = data.GetTag(delegate(IXmlTag tag) { return tag.GetTagName() == "value"; });
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