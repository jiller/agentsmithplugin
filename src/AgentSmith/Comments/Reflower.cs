using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgentSmith.Comments
{
    public class Reflower
    {
        private const int MAX_STRING_WIDTH = 80;
        
        private IEnumerable<Range> getWordsFromXmlComment(IDocCommentBlockNode docBlock)
        {
            StringBuilder reflownComment = new StringBuilder();
            if (docBlock != null)
            {
                XmlDocLexer lexer = new XmlDocLexer(docBlock);
                lexer.Start();
                int inCode = 0;
                while (lexer.TokenType != null)
                {
                    if (lexer.TokenType == lexer.XmlTokenType.TAG_START)
                    {
                        reflownComment.Add(lexer.TokenText);
                        lexer.Advance();
                        if (lexer.TokenType == lexer.XmlTokenType.IDENTIFIER &&
                            (lexer.TokenText == "code" || lexer.TokenText == "c"))
                        {
                            inCode++;
                        }
                        reflownComment.Add(lexer.TokenText);
                        lexer.Advance();
                        if (lexer.TokenType == lexer.XmlTokenType.TAG_END1)
                        {
                            inCode--;
                        }
                    }
                    if (lexer.TokenType == lexer.XmlTokenType.TAG_START1)
                    {
                        reflownComment.Add(lexer.TokenText);
                        lexer.Advance();
                        if (lexer.TokenType == lexer.XmlTokenType.IDENTIFIER &&
                            (lexer.TokenText == "code" || lexer.TokenText == "c"))
                        {
                            inCode--;
                        }
                    }
                    if (lexer.TokenType == lexer.XmlTokenType.TEXT && inCode == 0)
                    {
                        string currentLine = "";
                        lexer.TokenText;                        
                    }
                    else
                    {
                        reflownComment.Add(lexer.TokenText);
                    }
                    lexer.Advance();
                }
            }
        }       
    }
}
