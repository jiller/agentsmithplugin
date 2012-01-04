using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using AgentSmith.Options;

using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Caches;
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

        private readonly Regex _bulletItemRegex = new Regex(@"^\s*[-*]\s+(.*)$");
        private readonly Regex _numberItemRegex = new Regex(@"^\s*\d+\.?\s+(.*)$");
        private readonly Regex _paramRegex = new Regex(@"^\s*<\s*(?:type)?param\s+name\s*=\s*""([_a-zA-Z0-9]+)""");
        private readonly Regex _endParamRegex = new Regex(@"^\s*</\s*(?:type)?param\s*>");

        private readonly XmlDocumentationSettings _settings;

        public XmlCommentReflower(XmlDocumentationSettings settings)
        {
            _settings = settings;
        }

        public IEnumerable<Paragraph> Parse(IDocCommentBlockNode blockNode)
        {
            // Create a lexer which can read the comment
            XmlCommentReflowableBlockLexer lexer = new XmlCommentReflowableBlockLexer(blockNode);

            // Create a parser which can turn the comment into paragraphs and lines
            XmlCommentParagraphParser paragraphParser = new XmlCommentParagraphParser(lexer);

            // Firstly walk through the paragraphs and:
            // - collapse each one to the minimum number of required lines (tags always take a whole line at this stage).
            // - remove extra whitespace. There's no point in having it, it wont show in generated doco
            return paragraphParser.Parse();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blockNode"></param>
        /// <param name="maxLineLength"></param>
        /// <returns></returns>
        public string Reflow(IDocCommentBlockNode blockNode, int maxLineLength)
        {
            return ReflowToLineLength(Parse(blockNode), maxLineLength);
        }

        public string ReflowToLineLength(IEnumerable<Paragraph> paragraphs, int maxLineLength, IClassMemberDeclaration replaceIdentifiers = null)
        {
            LineBuilder lb = new LineBuilder();

            bool firstParagraph = true;
            foreach (Paragraph paragraph in paragraphs)
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
                                // append the space at the start of the line
                                if (lb.CurrentLine.Length == 0)
                                {
                                    lb.AppendMultilineBlock(paragraph.Offset);
                                }

                                string word = words[i];

                                if (replaceIdentifiers != null)
                                {
                                    ISolution solution = replaceIdentifiers.GetSolution();
                                    if ((IdentifierResolver.IsIdentifier(replaceIdentifiers, solution, word, DeclarationCacheLibraryScope.NONE) ||
                                         IdentifierResolver.IsKeyword(replaceIdentifiers, solution, word)) &&
                                        IdentifierResolver.AnalyzeForMetaTagging(word, _settings.CompiledWordsToIgnoreForMetatagging))
                                    {
                                        IList<string> replaceFormats = 
                                            IdentifierResolver.GetReplaceFormats(replaceIdentifiers, solution, word);

                                        string replacement = word;
                                        foreach (string format in replaceFormats)
                                        {
                                            replacement = String.Format(format, word);
                                            break;
                                        }

                                        word = replacement;
                                    }
                                }

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

        
        public List<Paragraph> GetClensedParagraphs(IEnumerable<Paragraph> paragraphs)
        {
            // Firstly walk through the paragraphs and:
            // - collapse each one to the minimum number of required lines (tags always take a whole line at this stage).
            // - remove extra whitespace. There's no point in having it, it wont show in generated doco
            List<Paragraph> clensedParagraphs = new List<Paragraph>();
            foreach (Paragraph paragraph in paragraphs)
            {
                Paragraph resultPara = new Paragraph();

                ParagraphLine resultLine = new ParagraphLine();
                foreach (ParagraphLine paragraphLine in paragraph.Lines)
                {

                    foreach (ParagraphLineItem lineItem in paragraphLine.Items)
                    {
                        // First deal with xml tags (note that c and code tags have been turned into non-reflowable blocks
                        if (lineItem.ItemType == ItemType.XmlElement)
                        {
                            // Finish the previous line

                            // Trim starting and ending spaces from the line
                            resultLine = resultLine.Trim();

                            // Only add non-empty ones
                            if (resultLine.Items.Count > 0) resultPara.Add(resultLine);

                            // Start a new line
                            resultLine = new ParagraphLine();

                            // Add the tag
                            resultLine.AddItem(lineItem);

                            // Add the tag line
                            resultPara.Add(resultLine);

                            // Start a new line
                            resultLine = new ParagraphLine();

                            continue;
                        }

                        // Now non-reflowable sections - just append them to the current line
                        if (lineItem.ItemType == ItemType.NonReflowableBlock)
                        {
                            resultLine.AddItem(lineItem);
                            continue;
                        }

                        // If it's a space:
                        // - Collapse multiple spaces down to a single space
                        if (lineItem.ItemType == ItemType.XmlSpace)
                        {
                            // If the previous item was a space then ignore this one.
                            ParagraphLineItem lastItem = resultLine.LastItem;
                            if (lastItem != null && lastItem.ItemType == ItemType.XmlSpace)
                            {
                                continue;
                            }

                            // Write a new, single space item to the line
                            resultLine.AddItem(new ParagraphLineItem { Text = " ", ItemType = ItemType.XmlSpace });
                            continue;
                        }

                        // If it's text then just append that to the previous line.
                        if (lineItem.ItemType == ItemType.Text)
                        {
                            // Normalise the whitespace first...
                            Regex re = new Regex(@"\s+");
                            lineItem.Text = re.Replace(lineItem.Text, " ").Trim();

                            // If it's a bullet or number item then save the paragraph and start a new one for this item.
                            if (_bulletItemRegex.IsMatch(lineItem.Text) || _numberItemRegex.IsMatch(lineItem.Text))
                            {
                                // Trim starting and ending spaces from the line
                                resultLine = resultLine.Trim();

                                // Only add non-empty ones
                                if (resultLine.Items.Count > 0)
                                {
                                    resultPara.Add(resultLine);
                                    clensedParagraphs.Add(resultPara);
                                    resultPara = new Paragraph();
                                }

                                // Start a new line
                                resultLine = new ParagraphLine();
                            }

                            resultLine.AddItem(lineItem);
                        }
                    }
                }

                // We'll have an unfinished line so close that off

                // Trim starting and ending spaces from the line
                resultLine = resultLine.Trim();

                // Add the line if it wasn't empty
                if (resultLine.Items.Count > 0) resultPara.Add(resultLine);

                // If the paragraph is empty then remove that too
                if (resultPara.Lines.Count == 0) continue;

                // Add the paragraph
                clensedParagraphs.Add(resultPara);
            }
            return clensedParagraphs;
        }

        private enum ListType
        {
            None,
            Bullet,
            Numbered
        }

        public List<Paragraph> InsertMissingTags(List<Paragraph> paragraphs)
        {
            // Go through the clean paragraphs and look for various things that should have xml tags but dont:
            // - Blocks of text without <para> tags
            // - Lines that start with a number and then a hyphen become numbered lists/list items
            // - lines that start with a hyphen or an asterisk become bulleted lists/list items

            List<Paragraph> results = new List<Paragraph>();

            ListType inList = ListType.None;
            bool inItem = false;

            for (int i = 0; i < paragraphs.Count; i++ )
            {
                Paragraph prevPara = i > 0 ? paragraphs[i - 1] : null;
                Paragraph para = paragraphs[i];
                Paragraph nextPara = i < paragraphs.Count - 1 ? paragraphs[i + 1] : null;

                Paragraph resultPara = new Paragraph();

                ParagraphLineItem prevItem = prevPara != null ? prevPara.Lines[0].Items[0] : null;
                ParagraphLineItem nextItem = nextPara != null ? nextPara.Lines[0].Items[0] : null;

                string prevTag = prevItem != null ? prevItem.Tag : null;
                string nextTag = nextItem != null ? nextItem.Tag : null;

                foreach (ParagraphLine line in para.Lines)
                {
                    // We're only interested in text and non-wrappable areas
                    ParagraphLineItem firstItem = line.Items[0];
                    ItemType itemType = firstItem.ItemType;
                    if (itemType != ItemType.Text && itemType != ItemType.NonReflowableBlock)
                    {
                        // If a list is in progress and this is an ending tag (ie for something before the list item) then we need to end the list.
                        if (inList != ListType.None && firstItem.IsEndTag)
                        {
                            if (inItem)
                            {
                                // End the previous item
                                ParagraphLine resultLine = new ParagraphLine();
                                resultLine.AddItem(
                                    new ParagraphLineItem
                                    {
                                        Text = "</description>",
                                        ItemType = ItemType.XmlElement
                                    });
                                resultPara.Add(resultLine);

                                resultLine = new ParagraphLine();
                                resultLine.AddItem(
                                    new ParagraphLineItem
                                    {
                                        Text = "</item>",
                                        ItemType = ItemType.XmlElement
                                    });
                                resultPara.Add(resultLine);

                                results.Add(resultPara);

                                resultPara = new Paragraph();
                                inItem = false;
                            }

                            ParagraphLine listEndLine = new ParagraphLine();
                            listEndLine.AddItem(
                                new ParagraphLineItem() { Text = "</list>", ItemType = ItemType.XmlElement });
                            resultPara.Add(listEndLine);
                            results.Add(resultPara);

                            resultPara = new Paragraph();

                            inList = ListType.None;

                        }

                        resultPara.Add(line);
                        continue;
                    }
                    
                    // If the line starts with a hyphen or asterisk then it's a bullet list item
                    ListType currentListType = ListType.None;
                    Match bulletItemMatch = _bulletItemRegex.Match(firstItem.Text);
                    string listItemText = null;
                    if (bulletItemMatch.Success)
                    {
                        currentListType = ListType.Bullet;
                        listItemText = bulletItemMatch.Groups[1].Value;
                    }

                    Match numberItemMatch = _numberItemRegex.Match(firstItem.Text);
                    if (numberItemMatch.Success)
                    {
                        currentListType = ListType.Numbered;
                        listItemText = numberItemMatch.Groups[1].Value;
                    }

                    // If we were previously in a list but now we're not (or we're in a different type) then close the old list
                    if (inList != ListType.None && inList != currentListType)
                    {
                        if (inItem)
                        {
                            // Close the old list item
                            ParagraphLine resultLine = new ParagraphLine();
                            resultLine.AddItem(
                                new ParagraphLineItem
                                {
                                    Text = "</description>",
                                    ItemType = ItemType.XmlElement
                                });
                            resultPara.Add(resultLine);

                            resultLine = new ParagraphLine();
                            resultLine.AddItem(
                                new ParagraphLineItem
                                {
                                    Text = "</item>",
                                    ItemType = ItemType.XmlElement
                                });
                            resultPara.Add(resultLine);
                            results.Add(resultPara);
                            resultPara = new Paragraph();

                            inItem = false;
                        }


                        ParagraphLine listEndLine = new ParagraphLine();
                        listEndLine.AddItem(
                            new ParagraphLineItem()
                            {
                                Text = "</list>",
                                ItemType = ItemType.XmlElement
                            });
                        resultPara.Add(listEndLine);
                        
                        results.Add(resultPara);

                        resultPara = new Paragraph(); 
                        
                        inList = ListType.None;
                    }


                    if (listItemText != null)
                    {
                        // Its a list item so do that.
                        ParagraphLine resultLine;

                        // If we weren't previously in a list (might have just ended) then start a new list
                        if (inList == ListType.None)
                        {
                            if (resultPara.Lines.Count > 0) results.Add(resultPara);

                            // New list
                            ParagraphLine listStartLine = new ParagraphLine();
                            listStartLine.AddItem(
                                new ParagraphLineItem()
                                    {
                                        Text =
                                            string.Format(
                                                "<list type=\"{0}\">",
                                                currentListType == ListType.Bullet ? "bullet" : "number"),
                                        ItemType = ItemType.XmlElement
                                    });
                            resultPara.Add(listStartLine);

                            results.Add(resultPara);

                            resultPara = new Paragraph();

                            inList = currentListType;
                        } else
                        {
                            if (inItem)
                            {
                                // End the previous item
                                resultLine = new ParagraphLine();
                                resultLine.AddItem(
                                    new ParagraphLineItem
                                    {
                                        Text = "</description>",
                                        ItemType = ItemType.XmlElement
                                    });
                                resultPara.Add(resultLine);

                                resultLine = new ParagraphLine();
                                resultLine.AddItem(
                                    new ParagraphLineItem
                                    {
                                        Text = "</item>",
                                        ItemType = ItemType.XmlElement
                                    });
                                resultPara.Add(resultLine);

                                results.Add(resultPara);

                                resultPara = new Paragraph();
                                inItem = false;

                            }

                        }

                        // Add this item
                        resultLine = new ParagraphLine();
                        resultLine.AddItem(
                            new ParagraphLineItem
                                {
                                    Text = "<item>",
                                    ItemType = ItemType.XmlElement
                                });
                        resultPara.Add(resultLine);

                        resultLine = new ParagraphLine();
                        resultLine.AddItem(
                            new ParagraphLineItem
                            {
                                Text = "<description>",
                                ItemType = ItemType.XmlElement
                            });
                        resultPara.Add(resultLine);

                        resultLine = new ParagraphLine();
                        resultLine.AddItem(
                            new ParagraphLineItem
                            {
                                Text = listItemText,
                                ItemType = ItemType.Text
                            });

                        for (int j = 1; j < line.Items.Count; j++ )
                        {
                            resultLine.AddItem(line.Items[j]);
                        }
                        resultPara.Add(resultLine);

                        inItem = true;

                        continue;
                    }

                    // IF the paragraph before and after are tags (and a matching pair) then do nothing
                    if (prevTag != null && prevTag == nextTag && !prevItem.IsEndTag && nextItem.IsEndTag)
                    {
                        resultPara.Add(line);
                        continue;
                    }
                    
                    // Wrap this in <para> tags
                    ParagraphLine start = new ParagraphLine();
                    start.AddItem(new ParagraphLineItem()
                        {
                            Text = "<para>",
                            ItemType = ItemType.XmlElement
                        });
                    resultPara.Add(start);

                    resultPara.Add(line);

                    ParagraphLine end = new ParagraphLine();
                    end.AddItem(new ParagraphLineItem()
                        {
                            Text = "</para>",
                            ItemType = ItemType.XmlElement
                        });
                    resultPara.Add(end);
                }

                // If there's still a list item open then close it
                if (inItem)
                {
                    // Close the old list item
                    ParagraphLine resultLine = new ParagraphLine();
                    resultLine.AddItem(
                        new ParagraphLineItem
                        {
                            Text = "</description>",
                            ItemType = ItemType.XmlElement
                        });
                    resultPara.Add(resultLine);

                    resultLine = new ParagraphLine();
                    resultLine.AddItem(
                        new ParagraphLineItem
                        {
                            Text = "</item>",
                            ItemType = ItemType.XmlElement
                        });
                    resultPara.Add(resultLine);

                    results.Add(resultPara);

                    resultPara = new Paragraph();

                    inItem = false;
                }

                results.Add(resultPara);
            }
            return results;
        }

        public string NewReflow(IClassMemberDeclaration declaration, List<Paragraph> paragraphs, int maxLineLength)
        {
            StringBuilder sb = new StringBuilder();
            string currentParam = null;
            ISolution solution = declaration.GetSolution();
            foreach (Paragraph paragraph in paragraphs)
            {
                // Now we expect that any "proper" tags will be on the outside (start/end) of the thing so build two versions of the reflowed line, one with separate lines for the tags
                StringBuilder singleLineBuilder = new StringBuilder();
                StringBuilder multiLineBuilder = new StringBuilder();
                int currentLineLength = 0;

                bool previousWasTag = false;
                bool firstLine = true;


                foreach (ParagraphLine line in paragraph.Lines)
                {

                    // First process the items and split text items into multiple single word blocks.
                    for (int i = 0; i < line.Items.Count; i++)
                    {
                        ParagraphLineItem item = line.Items[i];
                        if (item.ItemType == ItemType.XmlElement)
                        {
                            // Detect param's and typeparam's
                            Match match = _paramRegex.Match(item.Text);
                            if (match.Success) currentParam = match.Groups[1].Value;

                            // Detect the end of param's and typeparam's
                            match = _endParamRegex.Match(item.Text);
                            if (match.Success) currentParam = null;
                        }
                        if (item.ItemType == ItemType.Text)
                        {
                            // We know the words are all nicely separated by a single space 
                            // so we can just use split to get the list of words
                            string[] words = item.Text.Split(' ');
                            int j = 0;
                            line.Items.RemoveAt(i);
                            foreach (string word in words)
                            {
                                Regex subwordRegex = new Regex(@"\w+");

                                string param = currentParam; // Need to do this so our delegate below will work
                                string theWord = subwordRegex.Replace(word,
                                    delegate(Match match)
                                        {
                                            string matchWord = match.Captures[0].Value;

                                            if ((IdentifierResolver.IsIdentifier(declaration, solution, matchWord, DeclarationCacheLibraryScope.NONE) ||
                                                 IdentifierResolver.IsKeyword(declaration, solution, matchWord)) &&
                                                IdentifierResolver.AnalyzeForMetaTagging(matchWord, _settings.CompiledWordsToIgnoreForMetatagging))
                                            {
                                                // There are a few exceptions for auto tagging:
                                                switch (matchWord.ToLower())
                                                {
                                                    case "a": // Very annoying if you have a class called "A".
                                                    case "if": // There's some kind of system class called "If"
                                                    case "this": // This is an annoying keyword
                                                    case "else": // Can see this being an issue.
                                                    case "long": // Typically annoying
                                                    case "while":
                                                    case "lock":
                                                    case "fixed":
                                                    case "base":
                                                    case "object":
                                                        return matchWord;

                                                    default:

                                                        if (param != null && matchWord == param)
                                                        {
                                                            // Ignore the name of the current parameter.
                                                            break;
                                                        }

                                                        IList<string> replaceFormats =
                                                            IdentifierResolver.GetReplaceFormats(declaration, solution, matchWord);

                                                        string replacement = matchWord;
                                                        foreach (string format in replaceFormats)
                                                        {
                                                            replacement = String.Format(format, matchWord);
                                                            break;
                                                        }

                                                        return replacement;
                                                }

                                            }
                                            return matchWord;
                                        });



                                line.Items.Insert(i + j, new ParagraphLineItem { Text = theWord, ItemType = ItemType.Text});
                                j++;
                            }
                            i += j - 1;
                        }
                    }

                    foreach (ParagraphLineItem item in line.Items)
                    {
                        if (item.ItemType == ItemType.XmlElement)
                        {
                            singleLineBuilder.Append(item.Text);
                            if (currentLineLength != 0) multiLineBuilder.Append("\r\n");
                            multiLineBuilder.Append(item.Text);
                            multiLineBuilder.Append("\r\n");
                            currentLineLength = 0;
                            previousWasTag = true;
                            firstLine = false;
                        }
                        else
                        {
                            if (!previousWasTag && !firstLine) singleLineBuilder.Append(" ");
                            singleLineBuilder.Append(item.Text);
                            if (currentLineLength + item.Text.Length > maxLineLength && currentLineLength != 0)
                            {
                                multiLineBuilder.Append("\r\n");
                                currentLineLength = 0;
                            }
                            if (currentLineLength > 0)
                            {
                                multiLineBuilder.Append(" ");
                                currentLineLength++;
                            }
                            multiLineBuilder.Append(item.Text);
                            currentLineLength += item.Text.Length;
                            previousWasTag = false;
                            firstLine = false;
                        }

                    }

                    
                }
                if (currentLineLength > 0) multiLineBuilder.Append("\r\n");

                if (singleLineBuilder.Length > maxLineLength)
                {
                    sb.Append(multiLineBuilder.ToString());
                    continue;
                }

                if (singleLineBuilder.Length > 0) singleLineBuilder.Append("\r\n");
                sb.Append(singleLineBuilder.ToString());

            }
            return sb.ToString();
        }


        /// <summary>
        /// Reflow the given comment block to fit within the given maximum line length
        /// </summary>
        /// <param name="blockNode">The comment block to reflow</param>
        /// <param name="maxLineLength">The maximum line length</param>
        /// <returns>The text for the new reflown comment.</returns>
        public string ReflowAndRetag(IDocCommentBlockNode blockNode, int maxLineLength)
        {

            ITreeNode parent = blockNode.Parent;
            IClassMemberDeclaration parentDeclaration = parent as IClassMemberDeclaration;
            if (parentDeclaration == null)
            {
                IMultipleFieldDeclaration multipleFieldDeclaration = parent as IMultipleFieldDeclaration;
                if (multipleFieldDeclaration != null)
                {
                    foreach (IFieldDeclaration field in multipleFieldDeclaration.Children<IFieldDeclaration>())
                    {
                        parentDeclaration = field;
                        break;
                    }
                }
            }

            List<Paragraph> paragraphs = GetClensedParagraphs(Parse(blockNode));
            paragraphs = InsertMissingTags(paragraphs);

            return NewReflow(parentDeclaration, paragraphs, maxLineLength);
            /*LineBuilder lb = new LineBuilder();
            foreach (Paragraph para in paragraphs)
            {
                foreach (ParagraphLine line in para.Lines)
                {
                    bool first = true;
                    foreach (ParagraphLineItem item in line.Items)
                    {
                        if (!first) lb.Append(" ");
                        else first = false;
                        lb.Append(item.Text);
                        
                    }
                    lb.Append("\r\n");
                }
                lb.Append("\r\n");
            }
            return lb.ToString();
             */
        }
        
    }
}