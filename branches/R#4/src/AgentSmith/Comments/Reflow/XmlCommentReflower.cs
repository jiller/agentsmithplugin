using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgentSmith.Comments
{
    /// <summary>
    /// I want to hello
    /// ff <c> something safd dfsdf sdfsdf sdfsdf </c>
    /// <code>
    ///   adsfasdf
    ///    asdklfjlaksdfj
    /// asdfasdfasdf
    /// </code>
    /// <list type="fdfd"> <item>hello</item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// 1. All whitespace after a tag is preserved.
    /// 2. <c></c> and <code></code> are treated as non reflowable tags.
    /// 3. Blank lines or are treated as paragraph separators.
    /// 4. Line with indentation different from previous line is paragraph start.
    /// 4. XML tags are not split.
    /// 5. Bullet points are paragraph start.
    /// 6. Non reflowable expressions?
    /// </remarks>
    public class XmlCommentReflower
    {   
        private const int MAX_LINE_WIDTH = 80;
        private IEnumerable<string> getStrBlocks(string block)
        {
            int start = 0;
            while (IsSpace(block[start]) && start < block.Length)
                start++;
            
            yield return block.Substring(0, start);

            
            int end = block.Length - 1;
            while (IsSpace(block[end]) && end >= 0)
                end--;

            while (start < end)
            {
                int i = start;
                while (!IsSpace(block[i]) && i < end)
                {
                    i++;
                }
                
                if (i>start)
                    yield return block.Substring(start, i - start);
                
                while (IsSpace(block[i]) && i < end)
                {
                    i++;
                }

                if (i>start)
                    yield return block.Substring(start, i - start);
                start = i;
            }

            if (end < block.Length-1)
                yield return block.Substring(end + 1);
        }

        private bool IsSpace(char c)
        {
            return c == ' ' || c == '\n' || c=='\t';
        }

        public void Reflow()
        {
            LineBuilder lb = new LineBuilder();
            XmlCommentReflowableBlockLexer xmlBlockLexer = null;
            foreach (string block in xmlBlockLexer)
            {                
                if (block.StartsWith("<"))
                {
                    //This is xml element - just put it as it is.
                    lb.AppendMultilineBlock(strBlock, MAX_LINE_WIDTH);
                }
                else
                {                    
                    foreach (string strBlock in getStrBlocks(block))
                    {
                        if (!IsSpace(strBlock[0]))
                        {
                            lb.AppendMultilineBlock(strBlock, MAX_LINE_WIDTH);
                        }
                        else
                        {
                            foreach (char c in strBlock)
                            {
                                
                            }
                        }
                    }
                }
            }                                 
        }        
    }
}
