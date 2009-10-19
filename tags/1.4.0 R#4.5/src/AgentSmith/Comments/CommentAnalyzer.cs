using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AgentSmith.MemberMatch;
using AgentSmith.Options;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;
using Match=AgentSmith.MemberMatch.Match;

namespace AgentSmith.Comments
{      
    public class CommentAnalyzer : IDeclarationAnalyzer
    {
        private readonly CommentsSettings _settings;
        private readonly IList<Regex> _patternsToIgnore;
        private readonly ISolution _solution;
        private readonly ISpellChecker _spellChecker;

        public CommentAnalyzer(CommentsSettings settings, ISolution solution, IList<Regex> patternsToIgnore)
        {
            _settings = settings;
            _solution = solution;
            _patternsToIgnore = patternsToIgnore;
            _spellChecker = SpellCheckManager.GetSpellChecker(solution,
                                                              _settings.DictionaryName == null
                                                                  ? null
                                                                  : _settings.DictionaryName.Split(','));

            ComplexMatchEvaluator.Prepare(solution, _settings.CommentMatch, _settings.CommentNotMatch);
        }

        #region IDeclarationAnalyzer Members

        public SuggestionBase[] Analyze(IDeclaration declaration, bool spellCheck)
        {
            if (!(declaration is IClassMemberDeclaration) ||
                !CanBeSurroundedWithMetatagsSuggestion.Enabled && !WordIsNotInDictionarySuggestion.Enabled)
            {
                return new SuggestionBase[0];
            }

            List<SuggestionBase> highlightings = new List<SuggestionBase>();

            checkCommentSpelling((IClassMemberDeclaration) declaration, highlightings, spellCheck);
            checkMembersHaveComments((IClassMemberDeclaration) declaration, highlightings);

            return highlightings.ToArray();
        }

        #endregion

        private IDocCommentBlockNode getDocBlock(IClassMemberDeclaration decl)
        {
            IMultipleDeclarationMemberNode node = decl as IMultipleDeclarationMemberNode;
            if (node != null)
            {
                return SharedImplUtil.GetDocCommentBlockNode(node.MultipleDeclaration);
            }
            else
            {
                return SharedImplUtil.GetDocCommentBlockNode(decl.ToTreeNode());
            }
        }

        private void checkCommentSpelling(IClassMemberDeclaration decl,
                                          ICollection<SuggestionBase> highlightings, bool spellCheck)
        {
            if (_spellChecker == null)
            {
                return;
            }

            foreach (Range wordRange in getWordsFromXmlComment(getDocBlock(decl)))
            {
                bool keyword = isKeyword(wordRange.Word);
                if ((!SpellCheckUtil.ShouldSpellCheck(wordRange.Word) ||
                    _spellChecker.TestWord(wordRange.Word, true)) && 
                    !isKeyword(wordRange.Word))
                {                    
                    continue;
                }

                DocumentRange range = decl.GetContainingFile().GetDocumentRange(wordRange.TextRange);
                if (decl.DeclaredName != wordRange.Word)
                {
                    bool isIdentifier = IdentifierResolver.IsIdentifier(decl, _solution, wordRange.Word);
                    if (isIdentifier || keyword)
                    {
                        highlightings.Add(new CanBeSurroundedWithMetatagsSuggestion(wordRange.Word,
                                                                                    range, decl, _solution));
                    }
                
                    if (spellCheck)
                    {
                        checkWordSpelling(decl, wordRange, highlightings, range, !(isIdentifier || keyword));
                    }
                }
            }
        }

        private static readonly HashSet<string> keywords = new HashSet<string>
                                    {
                                        "foreach", "null", "true", "false"
                                    };

        private bool isKeyword(string word)
        {
            
            return keywords.Contains(word);
        }

        private void checkWordSpelling(IClassMemberDeclaration decl, Range wordRange,
                                       ICollection<SuggestionBase> highlightings, DocumentRange range, bool addCTag)
        {
            CamelHumpLexer camelHumpLexer = new CamelHumpLexer(wordRange.Word, 0, wordRange.Word.Length);
            foreach (LexerToken humpToken in camelHumpLexer)
            {
                if (SpellCheckUtil.ShouldSpellCheck(humpToken.Value) &&
                    !_spellChecker.TestWord(humpToken.Value, true))
                {
                    DocumentRange tokenRange = decl.GetContainingFile().GetDocumentRange(range.TextRange);

                    highlightings.Add(new WordIsNotInDictionarySuggestion(wordRange.Word, tokenRange,
                                                                          humpToken, _solution, _spellChecker, addCTag));

                    break;
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

                            lexer.Advance();
                            if (lexer.TokenType == lexer.XmlTokenType.TAG_END1)
                            {
                                inCode--;
                            }
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
                        string textWithoutPatterns = PatternRemover.RemovePatterns(lexer.TokenText, _patternsToIgnore);
                        ILexer wordLexer = new WordLexer(textWithoutPatterns);
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

        private void checkMembersHaveComments(IClassMemberDeclaration declaration,
                                              List<SuggestionBase> highlightings)
        {
            if (declaration is IConstructorDeclaration && declaration.IsStatic)
            {
                //TODO: probably need to put this somewhere in settings.
                //Static constructors have no visibility so not clear how to check them.
                return;
            }

            if (declaration.GetXMLDoc(_settings.SuppressIfBaseHasComment) == null)
            {
                if (declaration.DeclaredElement == null ||
                    declaration.DeclaredElement.GetXMLDoc(_settings.SuppressIfBaseHasComment) == null)
                {
                    Match match = ComplexMatchEvaluator.IsMatch(declaration,
                                                                _settings.CommentMatch, _settings.CommentNotMatch, true);

                    if (match != null)
                    {
                        FixCommentSuggestion suggestion = new FixCommentSuggestion(declaration, match);
                        highlightings.Add(suggestion);
                        return;
                    }
                }
            }
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