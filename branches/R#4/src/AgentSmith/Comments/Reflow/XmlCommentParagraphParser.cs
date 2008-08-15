﻿using System;
using System.Collections.Generic;

namespace AgentSmith.Comments.Reflow
{
    public class XmlCommentParagraphParser
    {
        private readonly XmlCommentReflowableBlockLexer  _xmlBlockLexer = null;
        
        public XmlCommentParagraphParser(XmlCommentReflowableBlockLexer xmlBlockLexer)
        {
            _xmlBlockLexer = xmlBlockLexer;
        }

        public IEnumerable<Paragraph> Parse()
        {
            Paragraph paragraph = new Paragraph();
            ParagraphLine previousLine = null;
            foreach (ParagraphLine line in readLines())
            {                              
                //xml element on start of line starts new paragraph
                ParagraphLine trimmedLine = line.TrimStart();
                ParagraphLine previousTrimmedLine = previousLine == null ? null : previousLine.Trim();
                if (trimmedLine.Items.Count > 0 && trimmedLine.Items[0].ItemType == ItemType.XmlElement)
                {
                    if (paragraph.Lines.Count > 0)
                        yield return paragraph;
                    paragraph = new Paragraph();
                    paragraph.Add(line);
                }
                //Anythyng after empty line starts new paragraph.
                else if (previousLine != null && previousTrimmedLine.Items.Count == 0)
                {
                    if (paragraph.Lines.Count > 0)
                        yield return paragraph;
                    paragraph = new Paragraph();
                    paragraph.Add(line);
                }
                //anything after xml element on own line starts new paragraph.
                else if (previousTrimmedLine != null && previousTrimmedLine.Items.Count == 1 && previousTrimmedLine.Items[0].ItemType == ItemType.XmlElement)
                {
                    if (paragraph.Lines.Count > 0)
                        yield return paragraph;
                    paragraph = new Paragraph();
                    paragraph.Add(line);
                }
                
                paragraph.Add(line);

                previousLine = line;
            }

            if (paragraph.Lines.Count > 0)
                yield return paragraph;
        }

        IEnumerable<ParagraphLine> readLines()
        {
            ParagraphLine paragraphLine = new ParagraphLine();            
            foreach (string block in _xmlBlockLexer)
            {
                ParagraphLineItem item = new ParagraphLineItem();                
                if (block.StartsWith("<code>") || block.StartsWith("<c>"))
                {
                    item.Text = block;
                    item.ItemType = ItemType.NonReflowableBlock;
                    paragraphLine.AddItem(item);                    
                }
                //TODO: is this true?
                else if (block.StartsWith("<"))
                {
                    item.Text = block;
                    item.ItemType = ItemType.XmlElement;
                    paragraphLine.AddItem(item);                    
                }
                else
                {
                    string[] lines = block.Replace("\r", "").Split('\n');
                    foreach (string line in lines)
                    {
                        item.Text = line;
                        item.ItemType = line.Trim().Length == 0 ? ItemType.Space : ItemType.Text;
                        paragraphLine.AddItem(item);
                        yield return paragraphLine;
                        paragraphLine = new ParagraphLine();
                    }
                }
            }

            if (paragraphLine.Items.Count > 0)
            {
                yield return paragraphLine;
            }
        }
    }
}
