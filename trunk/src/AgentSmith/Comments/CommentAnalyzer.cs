using System;
using System.Collections.Generic;
using AgentSmith.MemberMatch;
using AgentSmith.Options;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.ProjectModel;
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
        private readonly CommentsSettings _settings;
        private readonly ISolution _solution;
        private readonly ISpellChecker _spellChecker;

        public CommentAnalyzer(CommentsSettings settings, ISolution solution)
        {
            _settings = settings;
            _solution = solution;
            _spellChecker = SpellCheckManager.GetSpellChecker(solution);

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

        
        private  IDocCommentBlockNode getDocBlock(IClassMemberDeclaration decl)
        {
            if (decl is IMultipleDeclarationMemberNode)
            {
                return SharedImplUtil.GetDocCommentBlockNode(
                    ((IMultipleDeclarationMemberNode) decl).MultipleDeclaration);
            }
            else
            {
                return SharedImplUtil.GetDocCommentBlockNode(decl.ToTreeNode());
            }
        }

        #region IDeclarationAnalyzer Members

        public SuggestionBase[] Analyze(IDeclaration declaration)
        {
            if (!(declaration is IClassMemberDeclaration) ||
                !CanBeSurroundedWithMetatagsSuggestion.Enabled && !WordIsNotInDictionarySuggestion.Enabled)
            {
                return new SuggestionBase[0];
            }
            List<SuggestionBase> highlightings = new List<SuggestionBase>();
            checkCommentSpelling((IClassMemberDeclaration) declaration, highlightings);

            if (checkPublicMembersHaveComments((IClassMemberDeclaration) declaration, highlightings))
            {
                return highlightings.ToArray();
            }
            return highlightings.ToArray();
        }

        #endregion

        private void checkCommentSpelling(IClassMemberDeclaration decl,
                                          ICollection<SuggestionBase> highlightings)
        {
            if (_spellChecker == null)
            {
                return;
            }

            foreach (Range wordRange in getWordsFromXmlComment(getDocBlock(decl)))
            {
                if (SpellCheckUtil.ShouldSpellCheck(wordRange.Word) &&
                    !_spellChecker.TestWord(wordRange.Word, false))
                {
                    DocumentRange range = decl.GetContainingFile().GetDocumentRange(wordRange.TextRange);
                    if (IdentifierResolver.IsIdentifier(decl, _solution, wordRange.Word))
                    {
                        highlightings.Add(
                            new CanBeSurroundedWithMetatagsSuggestion(wordRange.Word, range, decl, _solution));
                    }
                    else
                    {
                        foreach (LexerToken humpToken in new CamelHumpLexer(wordRange.Word, 0, wordRange.Word.Length))
                        {
                            if (SpellCheckUtil.ShouldSpellCheck(humpToken.Value) &&
                                !_spellChecker.TestWord(humpToken.Value, false))
                            {
                                TextRange textRange = new TextRange(humpToken.Start + range.TextRange.StartOffset,
                                                                    humpToken.End + range.TextRange.StartOffset);

                                DocumentRange tokenRange = decl.GetContainingFile().GetDocumentRange(textRange);

                                highlightings.Add(
                                    new WordIsNotInDictionarySuggestion(humpToken.Value, tokenRange, _solution,
                                                                        _settings));
                                break;
                            }
                        }
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
                        if (lexer.TokenType == lexer.XmlTokenType.IDENTIFIER &&
                            (lexer.TokenText == "code" || lexer.TokenText == "c"))
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
                        if (lexer.TokenType == lexer.XmlTokenType.IDENTIFIER &&
                            (lexer.TokenText == "code" || lexer.TokenText == "c"))
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

        private bool checkPublicMembersHaveComments(IClassMemberDeclaration declaration,
                                                    List<SuggestionBase> highlightings)
        {
            if (declaration.GetXMLDoc(_settings.SuppressIfBaseHasComment) == null)
            {
                Match match =
                    ComplexMatchEvaluator.IsMatch(declaration, _settings.CommentMatch, _settings.CommentNotMatch, true);

                if (match != null)
                {
                    FixCommentSuggestion suggestion = new FixCommentSuggestion(declaration, match);
                    highlightings.Add(suggestion);
                    return true;
                }
            }
            return false;
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