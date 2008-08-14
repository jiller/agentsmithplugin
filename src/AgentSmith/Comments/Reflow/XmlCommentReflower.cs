using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith.Comments.Reflow
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
               
        public string Reflow(IDocCommentBlockNode blockNode)
        {
            LineBuilder lb = new LineBuilder();

            XmlCommentReflowableBlockLexer lexer = new XmlCommentReflowableBlockLexer(blockNode);

            XmlCommentParagraphParser paragraphParser = new XmlCommentParagraphParser(lexer);
            bool firstParagraph = true;
            foreach (Paragraph paragraph in paragraphParser.Parse())
            {
                if (!firstParagraph)
                {
                    lb.Append("\n");
                }
                foreach (ParagraphLine paragraphLine in paragraph.Lines)
                {
                    foreach (ParagraphLineItem lineItem in paragraphLine.Items)
                    {
                        if (lineItem.ItemType == ItemType.XmlElement ||
                            lineItem.ItemType == ItemType.NonReflowableBlock)
                        {
                            if (lb.CurrentLine.Length + lineItem.FirstLine.Length > MAX_LINE_WIDTH)
                                lb.AppendMultilineBlock("\n");
                            lb.AppendMultilineBlock(lineItem.Text);
                        }

                        if (lineItem.ItemType == ItemType.Space)
                        {
                            lb.AppendMultilineBlock(lineItem.Text);
                        }

                        if (lineItem.ItemType == ItemType.Text)
                        {
                            string[] words = lineItem.Text.Split(' ');
                            foreach (string word in words)
                            {
                                if (lb.CurrentLine.Length + word.Length > MAX_LINE_WIDTH)
                                    lb.AppendMultilineBlock("\n");
                                lb.AppendMultilineBlock(lineItem.Text);
                            }
                        }
                    }
                }
                firstParagraph = false;
            }

            return lb.ToString();
        }        
    }
}
