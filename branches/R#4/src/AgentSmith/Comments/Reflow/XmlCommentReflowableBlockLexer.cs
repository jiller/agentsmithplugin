using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith.Comments
{
    public class XmlCommentReflowableBlockLexer : IEnumerable<string>
    {
        private XmlDocLexer docLexer;

        public XmlCommentReflowableBlockLexer(IDocCommentBlockNode docCommentBlock)
        {
            docLexer = new XmlDocLexer(docCommentBlock);
            docLexer.Start();
        }

        public IEnumerator<string> GetEnumerator()
        {
            docLexer.Start();
            int inCode = 0;                      
            StringBuilder blockBuilder = new StringBuilder();
            while (docLexer.TokenType != null)
            {
                if (docLexer.TokenType == docLexer.XmlTokenType.TAG_START)
                {
                    if (blockBuilder.Length > 0 && inCode == 0)
                        yield return blockBuilder.ToString();
                    
                    blockBuilder.Remove(0, blockBuilder.Length);
                    
                    blockBuilder.Append(docLexer.TokenText);                    
                    docLexer.Advance();

                    if (docLexer.TokenType == docLexer.XmlTokenType.IDENTIFIER &&
                        (docLexer.TokenText == "code" || docLexer.TokenText == "c"))
                    {
                        inCode++;
                    }
                    
                    blockBuilder.Append(docLexer.TokenText);
                    docLexer.Advance();                                        
                }
                else if (docLexer.TokenType == docLexer.XmlTokenType.TAG_START1)
                {
                    if (blockBuilder.Length > 0 && inCode == 0)
                        yield return blockBuilder.ToString();

                    blockBuilder.Remove(0, blockBuilder.Length);

                    blockBuilder.Append(docLexer.TokenText);
                    docLexer.Advance();
                    if (docLexer.TokenType == docLexer.XmlTokenType.IDENTIFIER &&
                        (docLexer.TokenText == "code" || docLexer.TokenText == "c"))
                    {
                        inCode--;
                    }
                }
                else if (docLexer.TokenType == docLexer.XmlTokenType.TAG_END ||
                    docLexer.TokenType == docLexer.XmlTokenType.TAG_END1)
                {
                    if (docLexer.TokenType == docLexer.XmlTokenType.TAG_END1)
                    {
                        inCode--;
                    }
                    blockBuilder.Append(docLexer.TokenText);
                    if (inCode == 0)
                        yield return blockBuilder.ToString();

                    blockBuilder.Remove(0, blockBuilder.Length);
                    docLexer.Advance();
                }
                else
                {
                    blockBuilder.Append(docLexer.TokenText);
                    docLexer.Advance();
                }
                
            }

            yield return blockBuilder.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
