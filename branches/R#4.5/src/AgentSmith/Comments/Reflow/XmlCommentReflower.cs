using System;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith.Comments.Reflow
{
    /// <remarks>
    /// 1. All whitespace after a tag is preserved.
    /// 2. <c></c> and <code></code> are treated as non reflowable tags.
    /// 3. Blank lines or (?)are treated as paragraph separators.
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

                ParagraphLineItem previousItem = null;
                foreach (ParagraphLine paragraphLine in paragraph.Lines)
                {
                    foreach (ParagraphLineItem lineItem in paragraphLine.Items)
                    {
                        if (lineItem.ItemType == ItemType.XmlElement ||
                            lineItem.ItemType == ItemType.NonReflowableBlock)
                        {
                            if ( //if current line is empty, no matter how big text is, just append it (to 
                                // not create extra lines.
                                lb.CurrentLine.Trim().Length > 0 &&
                                //Append new line otherwise
                                lb.CurrentLine.Length + lineItem.FirstLine.Length > maxLineLength)
                            {
                                lb.AppendMultilineBlock("\r\n" + paragraph.Offset);
                            }
                            lb.AppendMultilineBlock(lineItem.Text);
                        }

                            //Space between XML elements is not reflown.
                        else if (lineItem.ItemType == ItemType.XmlSpace)
                        {
                            if (previousItem != null && previousItem.IsForcingNewLine)
                            {
                                // do not create new line if it is the last item
                                // in the paragraph
                                if ((lineItem != paragraphLine.Items[paragraphLine.Items.Count - 1])
                                    || (paragraphLine != paragraph.Lines[paragraph.Lines.Count - 1]))
                                {
                                    lb.AppendMultilineBlock(
                                        "\r\n" + paragraph.Offset);
                                }
                            }
                            else
                            {
                                lb.AppendMultilineBlock(lineItem.Text);
                            }
                        }

                        else if (lineItem.ItemType == ItemType.Text)
                        {
                            string text = lineItem.Text;
                            if (lineItem == paragraphLine.Items[0])
                            {
                                text = text.TrimStart();
                            }

                            if (previousItem != null && previousItem.IsForcingNewLine)
                            {
                                lb.AppendMultilineBlock("\r\n" + paragraph.Offset);
                                text = text.TrimStart();
                            }

                            string[] words = text.Split(' ');

                            for (int i = 0; i < words.Length; i++)
                            {
                                if (lb.CurrentLine.Length == 0)
                                {
                                    lb.AppendMultilineBlock(paragraph.Offset);
                                }

                                string word = words[i];

                                if (lb.CurrentLine.Length == paragraph.Offset.Length && word.Trim().Length == 0)
                                {
                                    continue;
                                }

                                //prepend space if this is not first word in block and not first word on paragraph line 
                                //or this is first word in block and block is appended to previous line.
                                bool previousBlockIsText = previousItem != null && previousItem.ItemType == ItemType.Text;
                                string toAppend = (lb.CurrentLine.Length > paragraph.Offset.Length && (i > 0 || previousBlockIsText) ? " " : "") + word;
                                if (lb.CurrentLine.Length + toAppend.Length > maxLineLength)
                                {
                                    lb.AppendMultilineBlock("\r\n" + paragraph.Offset);
                                    toAppend = word;
                                }

                                lb.AppendMultilineBlock(toAppend);
                            }
                        }
                        previousItem = lineItem;
                    }
                }
                firstParagraph = false;
            }

            return lb.ToString();
        }        
    }
}