using System;
using System.Collections.Generic;
using System.Xml;
using AgentSmith.Comments.NetSpell;
using AgentSmith.MemberMatch;
using AgentSmith.Options;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp.Stages;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentSmith.Comments
{
    public class CommentAnalyzer : IDeclarationAnalyzer
    {
        //private readonly bool _publicMembersMustHaveComments;
        private readonly CommentsSettings _settings;
        private readonly ISolution _solution;
        private readonly SpellChecker _spellChecker;

        public CommentAnalyzer(CommentsSettings settings, ISolution solution)
        {
            _settings = settings;
            //_publicMembersMustHaveComments = settings.PublicMembersMustHaveComments;
            _solution = solution;
            _spellChecker = SpellChecker.GetInstance(_settings.DictionaryName);
            if (_spellChecker != null)
            {
                _spellChecker.SetUserWords(settings.UserWords.Split('\n'));
            }

            if (_settings.CommentMatch != null)
            {
                foreach (Match match in _settings.CommentMatch)
                {
                    match.Prepare(solution, PsiManager.GetInstance(solution));
                }
            }
            if (_settings.CommentNotMatch != null)
            {
                foreach (Match match in _settings.CommentNotMatch)
                {
                    match.Prepare(solution, PsiManager.GetInstance(solution));
                }
            }
        }

        #region IDeclarationAnalyzer Members

        public CSharpHighlightingBase[] Analyze(IDeclaration declaration)
        {
            if (!(declaration is IClassMemberDeclaration))
            {
                return new CSharpHighlightingBase[0];
            }
            List<CSharpHighlightingBase> highlightings = new List<CSharpHighlightingBase>();
            checkCommentSpelling((IClassMemberDeclaration) declaration, highlightings);
            /* if (checkCommentIsCorrect(declaration))
             {
                 return _highlightings.ToArray();
             }*/
            if ( /*_publicMembersMustHaveComments &&*/
                checkPublicMembersHaveComments((IClassMemberDeclaration) declaration, highlightings))
            {
                return highlightings.ToArray();
            }
            return highlightings.ToArray();
        }

        public void Dispose()
        {
        }

        #endregion

        /// <summary>I I I me I 1
        ///  fff III I <c>aa</c> kkk<code/>precee        
        /// </summary>
        /// <param name="decl">asdfasdf1 aa 1</param>
        /// <param name="highlightings">hella23</param>
        /// <returns>It's me again</returns>
        private void checkCommentSpelling(IClassMemberDeclaration decl,
                                          ICollection<CSharpHighlightingBase> highlightings)
        {
            IDocCommentBlockNode docBlock = (this is IMultipleDeclarationMemberNode)
                                                ? SharedImplUtil.GetDocCommentBlockNode(
                                                      ((IMultipleDeclarationMemberNode) this).MultipleDeclaration)
                                                : SharedImplUtil.GetDocCommentBlockNode(decl.ToTreeNode());


            foreach (Range wordRange in getWordsFromXmlComment(docBlock))
            {
                if (wordRange.Word != wordRange.Word.ToUpper() && !containsDigit(wordRange.Word))
                {
                    if (_spellChecker != null && !_spellChecker.TestWord(wordRange.Word))
                    {
                        DocumentRange range = decl.GetContainingFile().GetDocumentRange(wordRange.TextRange);
                        WordIsNotInDictionarySuggestion highlighting =
                            new WordIsNotInDictionarySuggestion(wordRange.Word, range, _solution, _settings, decl);
                        highlightings.Add(highlighting);
                    }
                }
            }
        }

        private IEnumerable<Range> getWordsFromXmlComment(IDocCommentBlockNode docBlock)
        {
            if (docBlock != null)
            {
                XmlDocLexer lexer = new XmlDocLexer(docBlock);
                lexer.Start();
                int inCode = 0;
                while (lexer.TokenType != null)
                {
                    if (lexer.TokenType == lexer.XmlTokenType.TAG_START)
                    {
                        lexer.Advance();
                        if (lexer.TokenType == lexer.XmlTokenType.IDENTIFIER && (lexer.TokenText == "code" || lexer.TokenText=="c"))
                        {
                            inCode++;
                        }
                        lexer.Advance();
                        if (lexer.TokenType == lexer.XmlTokenType.TAG_END1)
                        {
                            inCode--;
                        }
                    }
                    if (lexer.TokenType == lexer.XmlTokenType.TAG_START1)
                    {
                        lexer.Advance();
                        if (lexer.TokenType == lexer.XmlTokenType.IDENTIFIER && (lexer.TokenText == "code" || lexer.TokenText == "c"))
                        {
                            inCode--;
                        }
                    }
                    if (lexer.TokenType == lexer.XmlTokenType.TEXT && inCode == 0)
                    {
                        ILexer wordLexer = new WordLexer(lexer.TokenText);
                        wordLexer.Start();
                        while (wordLexer.TokenType != null)
                        {
                            int start = lexer.TokenLocalRange.StartOffset + wordLexer.TokenStart;
                            int end = start + wordLexer.TokenText.Length;
                            yield return new Range(wordLexer.TokenText, new TextRange(start, end));

                            wordLexer.Advance();
                        }
                    }
                    lexer.Advance();
                }
            }
        }

        private static bool containsDigit(string text)
        {
            foreach (char c in text)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }

        private bool checkCommentIsCorrect(IClassMemberDeclaration decl, IList<IHighlighting> highlightings)
        {
            if (decl.GetXMLDoc(false) != null)
            {
                XmlNodeList nodes = decl.GetXMLDoc(false).SelectNodes("summary | param | remarks");
                foreach (XmlElement el in nodes)
                {
                    if (el != null)
                    {
                        string text = el.InnerText.Trim();
                        if (text.Length == 0 || !char.IsUpper(text[0]) || !text.EndsWith("."))
                        {
                            highlightings.Add(
                                new FixCommentSuggestion(decl,
                                                         "Comment should start with captial letter and end with a period."));
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool checkPublicMembersHaveComments(IClassMemberDeclaration decl,
                                                    List<CSharpHighlightingBase> highlightings)
        {
            if (decl.GetXMLDoc(_settings.SuppressIfBaseHasComment) == null)
            {
                Match match =
                    ComplexMatchEvaluator.IsMatch(decl, _settings.CommentMatch, _settings.CommentNotMatch, true);

                if (match != null)
                {
                    FixCommentSuggestion suggestion =
                        new FixCommentSuggestion(decl, match + "should have XML comment.");
                    highlightings.Add(suggestion);
                    return true;
                }
            }
            return false;
            /*if (decl.GetAccessRights() != AccessRights.PRIVATE &&
                decl.GetXMLDoc(true) == null)
            {
                bool shouldAddComment = true;
                ICSharpTypeDeclaration type = decl.GetContainingTypeDeclaration();
                if (type != null && type.IsSealed && decl.GetAccessRights() == AccessRights.PROTECTED)
                {
                    shouldAddComment = false;
                }
                else
                {
                    while (type != null)
                    {
                        if (type.GetAccessRights() == AccessRights.PRIVATE)
                        {
                            shouldAddComment = false;
                            break;
                        }
                        type = type.GetContainingTypeDeclaration();
                    }
                }

                if (shouldAddComment)
                {
                    FixCommentSuggestion suggestion = new FixCommentSuggestion(decl, "Non private member should have comment.");
                    highlightings.Add(suggestion);
                    return true;
                }
            }*/
            //return false;
        }

        #region Nested type: Range

        private struct Range
        {
            public readonly TextRange TextRange;
            public readonly string Word;


            public Range(string word, TextRange range)
            {
                Word = word;
                TextRange = range;
            }
        }

        #endregion
    }
}