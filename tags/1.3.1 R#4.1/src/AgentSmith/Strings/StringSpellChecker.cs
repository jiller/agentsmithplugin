using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using AgentSmith.Comments;
using AgentSmith.Options;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;
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
                StringBuilder sb = new StringBuilder(text.Length);
                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == '\\' && i + 1 < text.Length)
                    {
                        char c = text[i + 1];
                        if (c == '\\' || c == 'r' || c == 'n' || c == 't' ||
                            c == 'a' || c == 'b' || c == 'f' || c == 'v')
                        {
                            sb.Append("  ");
                            i++;
                        }
                        else
                        {
                            sb.Append(text[i]);
                        }
                    }
                    else
                    {
                        sb.Append(text[i]);
                    }
                }

                return sb.ToString();
            }
            return text;
        }

        public static IList<SuggestionBase> SpellCheck(IDocument document, ITokenNode token, ISpellChecker spellChecker,
                                                       ISolution solution, List<Regex> patternsToIgnore)
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
            buffer = PatternRemover.RemovePatterns(buffer, patternsToIgnore);

            ILexer wordLexer = new WordLexer(buffer);
            wordLexer.Start();
            while (wordLexer.TokenType != null)
            {
                if (SpellCheckUtil.ShouldSpellCheck(wordLexer.TokenText) &&
                    !spellChecker.TestWord(wordLexer.TokenText.Replace("&", ""), true))
                {
                    IClassMemberDeclaration containingElement =
                        token.GetContainingElement<IClassMemberDeclaration>(false);
                    if (containingElement == null ||
                        !IdentifierResolver.IsIdentifier(containingElement, solution, wordLexer.TokenText))
                    {
                        CamelHumpLexer camelHumpLexer = new CamelHumpLexer(buffer, wordLexer.TokenStart,
                                                                           wordLexer.TokenEnd);
                        foreach (LexerToken humpToken in camelHumpLexer)
                        {
                            if (SpellCheckUtil.ShouldSpellCheck(humpToken.Value) &&
                                !spellChecker.TestWord(humpToken.Value, true))
                            {
                                int start = token.GetTreeStartOffset() + wordLexer.TokenStart;
                                int end = start + wordLexer.TokenText.Length;

                                TextRange range = new TextRange(start, end);
                                DocumentRange documentRange = new DocumentRange(document, range);
                                TextRange textRange = new TextRange(humpToken.Start - wordLexer.TokenStart,
                                                                    humpToken.End - wordLexer.TokenStart);
                                
                                suggestions.Add(new StringSpellCheckSuggestion(document.GetText(range), documentRange,
                                                                               humpToken.Value, textRange,
                                                                               solution, spellChecker));
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