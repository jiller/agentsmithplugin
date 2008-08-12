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
            while (char.IsSeparator(block[start]) && start < block.Length)
                start++;
            
            yield return block.Substring(0, start);

            int end = block.Length - 1;
            while (char.IsSeparator(block[end]) && end >= 0)
                end--;
                       
            while (!char.IsSeparator(block[start]) && start < end)
            {

            }

            if (end < block.Length-1)
                yield return block.Substring(end + 1);
        }

        public void Reflow()
        {
            StringBuilder sb = new StringBuilder();
            XmlCommentReflowableBlockLexer xmlBlockLexer = null;
            foreach (string block in xmlBlockLexer)
            {                
                if (block.StartsWith("<"))
                {
                    //This is xml element - just put it as it is.
                    sb.Append(block);
                }
                else
                {
                    //Add leading spaces etc.
                    int i = 0;
                    while (char.IsSeparator(block[i]) && i < block.Length)
                        i++;
                    sb.Append(block.Substring(0, i));

                    while (!char.IsSeparator(block[i]) && i < block.Length)
                    {
                        
                    }
                }
            }
            
            string currentLine = "";
            foreach (string block in getBlocks())
            {
                for (int i = 0; i < getEmptyLinesCount(block); i++)
                {
                    sb.AppendLine();
                    currentLine = String.Empty;
                }
                
                if (currentLine.Length + block.Length <= MAX_LINE_WIDTH)
                {
                    currentLine += block;
                }
                else
                {
                    sb.AppendLine(block);
                    currentLine = block.TrimStart();
                }
            }

            if (currentLine.Trim().Length > 0)
            {
                sb.Append(currentLine);
            }
        }        
    }
}
