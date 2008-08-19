using System;
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
        public string Reflow(IDocCommentBlockNode blockNode, int maxLineLength)
        {            
            LineBuilder lb = new LineBuilder();

            XmlCommentReflowableBlockLexer lexer = new XmlCommentReflowableBlockLexer(blockNode);

            XmlCommentParagraphParser paragraphParser = new XmlCommentParagraphParser(lexer);
            bool firstParagraph = true;
            foreach (Paragraph paragraph in paragraphParser.Parse())
            {
                if (!firstParagraph)
                {
                    lb.Append("\r\n");
                }
                foreach (ParagraphLine paragraphLine in paragraph.Lines)
                {                    
                    foreach (ParagraphLineItem lineItem in paragraphLine.Items)
                    {
                        if (lineItem.ItemType == ItemType.XmlElement ||
                            lineItem.ItemType == ItemType.NonReflowableBlock)
                        {
                            if (lb.CurrentLine.Length + lineItem.FirstLine.Length > maxLineLength)
                                lb.AppendMultilineBlock("\r\n");
                            lb.AppendMultilineBlock(lineItem.Text);
                        }

                        if (lineItem.ItemType == ItemType.XmlSpace)
                        {
                            lb.AppendMultilineBlock(lineItem.Text);
                        }

                        if (lineItem.ItemType == ItemType.Text)
                        {
                            string text = lineItem.Text;
                            if (lineItem == paragraphLine.Items[0])
                            {
                                text = text.TrimStart();
                                if (lb.CurrentLine.Length > 0)
                                    lb.AppendMultilineBlock(" ");
                            }
                            string[] words = text.Split(' ');

                            for (int i=0; i<words.Length; i++)
                            {
                                if (lb.CurrentLine.Length == 0)
                                    lb.AppendMultilineBlock(paragraph.Offset);
                                
                                string word = words[i];
                                if (lb.CurrentLine.Length + word.Length > maxLineLength)
                                    lb.AppendMultilineBlock("\r\n" + paragraph.Offset);
                                lb.AppendMultilineBlock(word + ((i!=words.Length-1)?" ": ""));
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
