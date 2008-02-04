using System;
using System.Collections.Generic;
using AgentSmith.Comments;
using AgentSmith.Options;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentSmith.Strings
{
    internal static class StringSpellChecker
    {
        private static string unescape(string text)
        {
            if (!text.StartsWith("@"))
            {
                //TODO: optimize this
                return text.
                    Replace("\\\\", "  ").
                    Replace("\\a", "  ").
                    Replace("\\b", "  ").
                    Replace("\\f", "  ").
                    Replace("\\n", "  ").
                    Replace("\\r", "  ").
                    Replace("\\t", "  ").
                    Replace("\\v", "  ");
            }
            return text;
        }

        public static IList<SuggestionBase> SpellCheck(IDocument document, ITokenNode token, ISpellChecker spellChecker,
                                                       ISolution solution)
        {
            List<SuggestionBase> suggestions = new List<SuggestionBase>();
            if (spellChecker == null)
            {
                return suggestions;
            }

            CodeStyleSettings settings = CodeStyleSettings.GetInstance(solution);
            if (settings == null)
            {
                //TODO:This might happen if plugin is activated manually
                return suggestions;
            }

            string buffer = unescape(token.GetText());
            ILexer wordLexer = new WordLexer(buffer);
            wordLexer.Start();
            while (wordLexer.TokenType != null)
            {
                if (SpellCheckUtil.ShouldSpellCheck(wordLexer.TokenText) &&
                    !spellChecker.TestWord(wordLexer.TokenText, false))
                {
                    IClassMemberDeclaration containingElement =
                        token.GetContainingElement<IClassMemberDeclaration>(false);
                    if (containingElement == null ||
                        !IdentifierResolver.IsIdentifier(containingElement, solution, wordLexer.TokenText))
                    {
                        foreach (LexerToken humpToken in new CamelHumpLexer(buffer, wordLexer.TokenStart, wordLexer.TokenEnd))
                        {
                            if (SpellCheckUtil.ShouldSpellCheck(humpToken.Value) &&
                                !spellChecker.TestWord(humpToken.Value, false))
                            {
                                int start = token.GetTreeStartOffset() + wordLexer.TokenStart;
                                int end = start + wordLexer.TokenText.Length;

                                TextRange range = new TextRange(start, end);
                                DocumentRange documentRange = new DocumentRange(document, range);

                                suggestions.Add(new StringSpellCheckSuggestion(document.GetText(range), documentRange,
                                    humpToken.Value, new TextRange(humpToken.Start - wordLexer.TokenStart, humpToken.End - wordLexer.TokenStart),
                                    solution, settings.CommentsSettings));
                                break;
                            }
                        }
                    }
                }

                wordLexer.Advance();
            }
            return suggestions;
        }
    }
}