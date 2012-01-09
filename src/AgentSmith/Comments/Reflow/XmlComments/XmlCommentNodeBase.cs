using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using AgentSmith.Options;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Caches;

namespace AgentSmith.Comments.Reflow.XmlComments
{

    public class XmlCommentOptions
    {
        public IClassMemberDeclaration Declaration;

        public ISolution Solution;

        public IdentifierLookupScopes IdentifierLookupScope;

        public IEnumerable<Regex> IdentifiersToIgnoreForMetaTagging;

    }

    public interface IXmlCommentNode
    {
        void FromXml(XmlNode node);

        void InsertMissingTags();

        string ToXml(int currentLineLength, int maxLineLength);
    }

    public abstract class XmlCommentNodeBase : IXmlCommentNode
    {
        public XmlCommentOptions Options { get; private set; }

        protected XmlCommentNodeBase(XmlCommentOptions options)
        {
            Options = options;
        }

        public abstract void FromXml(XmlNode node);
        public abstract void InsertMissingTags();
        public abstract string ToXml(int currentLineLength, int maxLineLength);
    }

    public class XmlComment : XmlCommentNodeBase
    {
        public SummaryNode Summary { get; set; }
        public RemarksNode Remarks { get; set; }
        public ValueNode Value { get; set; }
        public List<ExampleNode> Examples { get; set; }
        public List<TypeParamNode> TypeParams { get; set; }
        public List<ParamNode> Params { get; set; }
        public List<ExceptionNode> Exceptions { get; set; }
        public List<PermissionNode> Permissions { get; set; }
        public ReturnsNode Returns { get; set; }
        public List<UnknownTagNode> UnknownTagNodes { get; set; }

        public XmlComment(XmlCommentOptions options) : base(options)
        {
            Examples = new List<ExampleNode>();
            TypeParams = new List<TypeParamNode>();
            Params = new List<ParamNode>();
            Exceptions = new List<ExceptionNode>();
            Permissions = new List<PermissionNode>();
            UnknownTagNodes = new List<UnknownTagNode>();
        }
        public override void FromXml(XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                XmlElement element = child as XmlElement;
                if (element != null)
                {
                    switch (element.Name.ToLower())
                    {
                        case "summary":
                            if (Summary == null) Summary = new SummaryNode(Options);
                            Summary.FromXml(element);
                            break;
                        case "remark":
                        case "remarks":
                            if (Remarks == null) Remarks = new RemarksNode(Options);
                            Remarks.FromXml(element);
                            break;
                        case "value":
                            if (Value == null) Value = new ValueNode(Options);
                            Value.FromXml(element);
                            break;
                        case "example":
                            ExampleNode example = new ExampleNode(Options);
                            example.FromXml(element);
                            Examples.Add(example);
                            break;
                        case "typeparam":
                            TypeParamNode typeParam = new TypeParamNode(Options);
                            typeParam.FromXml(element);
                            TypeParams.Add(typeParam);
                            break;
                        case "param":
                            ParamNode param = new ParamNode(Options);
                            param.FromXml(element);
                            Params.Add(param);
                            break;
                        case "exception":
                            ExceptionNode exception = new ExceptionNode(Options);
                            exception.FromXml(element);
                            Exceptions.Add(exception);
                            break;
                        case "permissions":
                            PermissionNode permission = new PermissionNode(Options);
                            permission.FromXml(element);
                            Permissions.Add(permission);
                            break;
                        case "result":
                        case "return":
                        case "returns":
                            if (Returns == null) Returns = new ReturnsNode(Options);
                            Returns.FromXml(element);
                            break;
                        default:
                            // If we see another tag then they should have escaped it but for now just maintain it
                            string outerXml = element.OuterXml;
                            string startTag = outerXml.Substring(0, outerXml.IndexOf('>') + 1).Trim();
                            //startTag = startTag.Replace("<", "&lt;").Replace(">", "&gt;");
                            string endTag = element.IsEmpty? "" : string.Format("</{0}>", element.LocalName);
                            UnknownTagNode unknown = new UnknownTagNode(Options) { StartTag = startTag, EndTag = endTag };
                            unknown.FromXml(element);
                            UnknownTagNodes.Add(unknown);
                            break;
                    }
                    continue;
                }

                // Not an element - dunno what to do with it
            }
        }

        public override void InsertMissingTags()
        {
            if (Summary != null) Summary.InsertMissingTags();
            if (Remarks != null) Remarks.InsertMissingTags();
            if (Value != null) Value.InsertMissingTags();
            foreach (var child in Examples) child.InsertMissingTags();
            foreach (var child in TypeParams) child.InsertMissingTags();
            foreach (var child in Params) child.InsertMissingTags();
            foreach (var child in Exceptions) child.InsertMissingTags();
            foreach (var child in Permissions) child.InsertMissingTags();
            if (Returns != null) Returns.InsertMissingTags();
        }

        public override string ToXml(int currentLineLength, int maxLineLength)
        {
            StringBuilder sb = new StringBuilder();

            if (Summary != null)
            {
                sb.Append(Summary.ToXml(0, maxLineLength));
                sb.Append("\n");
            }
            if (Remarks != null)
            {
                sb.Append(Remarks.ToXml(0, maxLineLength));
                sb.Append("\n");
            }
            if (Value != null)
            {
                sb.Append(Value.ToXml(0, maxLineLength));
                sb.Append("\n");
            }
            if (Examples.Count > 0)
            {
                foreach (var child in Examples)
                {
                    sb.Append(child.ToXml(0, maxLineLength));
                    sb.Append("\n");
                }
            }
            if (TypeParams.Count > 0)
            {
                foreach (var child in TypeParams)
                {
                    sb.Append(child.ToXml(0, maxLineLength));
                    sb.Append("\n");
                }
            }
            if (Params.Count > 0)
            {
                foreach (var child in Params)
                {
                    sb.Append(child.ToXml(0, maxLineLength));
                    sb.Append("\n");
                }
            }
            if (Exceptions.Count > 0)
            {
                foreach (var child in Exceptions)
                {
                    sb.Append(child.ToXml(0, maxLineLength));
                    sb.Append("\n");
                }
            }
            if (Permissions.Count > 0)
            {
                foreach (var child in Permissions)
                {
                    sb.Append(child.ToXml(0, maxLineLength));
                    sb.Append("\n");
                }
            }
            if (Returns != null)
            {
                sb.Append(Returns.ToXml(0, maxLineLength));
                sb.Append("\n");
            }
            if (UnknownTagNodes.Count > 0)
            {
                foreach (var child in UnknownTagNodes)
                {
                    sb.Append(child.ToXml(0, maxLineLength));
                    sb.Append("\n");
                }
            }
            string result = sb.ToString();
            if (result.EndsWith("\n")) result = result.Substring(0, result.Length - 1);

            return result;
        }
    }

    public abstract class ExtendedBlockNode : XmlCommentNodeBase
    {
        private readonly Regex _bulletItemRegex = new Regex(@"^[-*]$");
        private readonly Regex _numberItemRegex = new Regex(@"^\d+\.?$");
        private readonly Regex _whitespaceRegex = new Regex(@"\s+");

        public List<IXmlCommentNode> Contents { get; set; }

        protected ExtendedBlockNode(XmlCommentOptions options) : base(options)
        {
            Contents = new List<IXmlCommentNode>();
        }

        public abstract string GetStartTag();
        public abstract string GetEndTag();

        public virtual bool AllowLineCollapse { get { return true; } }

        public virtual IEnumerable<Regex> IdentifiersToIgnore
        {
            get
            {
                return Options.IdentifiersToIgnoreForMetaTagging;
            }
        }

        public override string ToXml(int currentLineLength, int maxLineLength)
        {
            StringBuilder sb = new StringBuilder();
            int currentChars = currentLineLength;
            bool first = currentChars == 0;
            foreach (var child in Contents)
            {
                // Is it a word?
                if (child is WordNode)
                {
                    // Yep

                    // Get the word text so we know how long it will be.
                    string childText = child.ToXml(0, maxLineLength);

                    // Are we the first text?
                    if (first)
                    {
                        // Yep

                        // Clear that
                        first = false;

                        // Append our text (no matter how long)
                        sb.Append(childText);

                        // Set the length
                        currentChars += childText.Length;
                    }
                    else
                    {
                        // Not the first

                        // Will this text overflow the line?
                        if (currentChars + childText.Length + 1 > maxLineLength)
                        {
                            // Yes

                            // If the word is so big it fills the line then write it anyway
                            if (currentChars == 0)
                            {
                                sb.Append(childText);
                                sb.Append("\n");
                                currentChars = 0;
                                continue;
                            }

                            // Break the line here
                            sb.Append("\n");

                            // And put the text on the next line
                            sb.Append(childText);

                            // And reset the length
                            currentChars = childText.Length;
                        }
                        else
                        {
                            // No

                            // Add a space
                            sb.Append(" ");

                            // Add the text
                            sb.Append(childText);

                            // Add to the length
                            currentChars += 1 + childText.Length;
                        }
                    }
                    continue;
                    
                }

                // Not a word, so should be an element of some kind.

                // If it's one that starts a new line then do that.
                if (child is ExampleNode ||
                    child is ParaNode ||
                    child is ListNode ||
                    child is CodeNode)
                {

                    // If its a paragraph and it's the only child of the parent then take out the tags
                    ParaNode para = child as ParaNode;
                    if (para != null && Contents.Count == 1)
                    {
                        para.NoTags = true;
                        // Also, in no tag mode, we dont put the start/end new line
                    }
                    else
                    {
                        // End the previous line (if there was one).
                        if (first) first = false;
                        else if (sb[sb.Length - 1] != '\n')
                        {
                            sb.Append("\n");
                        }
                    }

                    // Write the sub element
                    sb.Append(child.ToXml(0, maxLineLength));

                    // And start a new line
                    if (para == null || !para.NoTags) sb.Append("\n");
                              
                    // And reset the count
                    currentChars = 0;

                    continue;
                }

                // It's an inline block

                // Get the text assuming it will be on this line (include extra 1 for space).
                string inlineText = child.ToXml(currentChars + 1, maxLineLength);

                // Does it contain newlines?
                if (inlineText.Contains("\n"))
                {
                    // Yes

                    // So work out the length of the unterminated line.
                    int lastLineLength = inlineText.Length - inlineText.LastIndexOf('\n');

                    // Append space
                    if (first) first = false;
                    else
                    {
                        sb.Append(" ");
                    }

                    // Append the text.
                    sb.Append(inlineText);

                    // Set the line length
                    currentChars = lastLineLength;
                    continue;
                }

                // Append space
                if (first) first = false;
                else
                {
                    sb.Append(" ");
                }

                // No newlines so its a continuation of the current line
                sb.Append(inlineText);
                currentChars += inlineText.Length;
            }

            // Get the total contents
            string contents = sb.ToString();
            if (contents.EndsWith("\n"))
            {
                contents = contents.TrimEnd();
            }

            // And get the start and end tags
            string startTag = GetStartTag();
            string endTag = GetEndTag();

            // See if it'll all fit on one line
            if (contents.Contains("\n") ||
                startTag.Length + contents.Length + endTag.Length > maxLineLength - currentLineLength ||
                !AllowLineCollapse)
            {
                // doesn't fit on one line so split the tags onto separate lines

                // Make sure the tags actually have a value
                if (startTag == "" && endTag == "")
                {
                    return contents;
                }
                if (contents == "" && endTag == "")
                {
                    return string.Format("{0}", startTag);
                }

                return string.Format("{0}\n{1}\n{2}", startTag, contents, endTag);
            }

            if (contents == "" && endTag == "")
            {
                return string.Format("{0}", startTag);
            }
            
            // Should all fit on one line so do that
            return string.Format("{0}{1}{2}", startTag, contents, endTag);

        }

        public override void InsertMissingTags()
        {
            //foreach (var child in Contents) child.InsertMissingTags();

            List<IXmlCommentNode> newContents = new List<IXmlCommentNode>();

            List<IXmlCommentNode> currentLine = new List<IXmlCommentNode>();
            ListNode list = null;
            DescriptionNode desc = null;
            bool lineStart = true;
            foreach (IXmlCommentNode xmlCommentNode in Contents)
            {
                // Is it a new line
                NewLineNode newLine = xmlCommentNode as NewLineNode;
                if (newLine != null)
                {
                    // Yep

                    // Are we at the start of a line?
                    if (lineStart)
                    {
                        // Double new line

                        if (list == null)
                        {
                            // not in a list, so this the end of a paragraph and the start of something else
                            if (currentLine.Count > 0)
                            {
                                ParaNode para = new ParaNode(Options);
                                para.Contents = currentLine;
                                newContents.Add(para);
                                currentLine = new List<IXmlCommentNode>();
                            }
                        }
                        else
                        {
                            // We are in a list

                            // End the last item if there was one
                            if (desc != null && desc.Contents.Count > 0)
                            {
                                ItemNode item = new ItemNode(Options);
                                item.Description = desc;
                                list.Items.Add(item);
                                desc = null;
                            }

                        }
                    }
                    lineStart = true;
                    continue;
                }

                WordNode word = xmlCommentNode as WordNode;
                if (word != null)
                {
                    if (lineStart)
                    {
                        // At the start of a line

                        bool isBullet = _bulletItemRegex.IsMatch(word.Word);
                        bool isNumber = _numberItemRegex.IsMatch(word.Word);

                        // See if it's a list item
                        if (isBullet || isNumber)
                        {

                            // This is a new list item.

                            // Check if we're already in a list and, if not then start a new one.
                            if (list == null)
                            {
                                // Not in a list - write the previous paragraph
                                if (currentLine.Count > 0)
                                {
                                    ParaNode para = new ParaNode(Options);
                                    para.Contents = currentLine;
                                    newContents.Add(para);
                                    currentLine = new List<IXmlCommentNode>();
                                }

                                list = new ListNode(Options);
                                if (isBullet)
                                {
                                    list.Type = ListTypes.Bullet;
                                } else
                                {
                                    list.Type = ListTypes.Number;
                                }
                            }
                            else
                            {
                                // Do we have a previous item?
                                if (desc != null && desc.Contents.Count > 0)
                                {
                                    // yep so close that
                                    ItemNode item = new ItemNode(Options);
                                    item.Description = desc;
                                    list.Items.Add(item);
                                }

                                // Is the new item the same type as the old list?
                                if ((isBullet && list.Type != ListTypes.Bullet) ||
                                    (isNumber && list.Type != ListTypes.Number))
                                {
                                    // Different list type so end the old list and start a new one.
                                    if (list.Items.Count > 0) newContents.Add(list);
                                    list = new ListNode(Options);
                                    if (isBullet)
                                    {
                                        list.Type = ListTypes.Bullet;
                                    }
                                    else
                                    {
                                        list.Type = ListTypes.Number;
                                    }

                                }
                            }

                            // Create the new item
                            desc = new DescriptionNode(Options);
                        }
                        else
                        {
                            // Just a regular word.
                            
                            // See if it's an identifier
                            if (IdentifierResolver.IsIdentifier(Options.Declaration, Options.Solution, word.Word, Options.IdentifierLookupScope ) ||
                                IdentifierResolver.IsKeyword(Options.Declaration, Options.Solution, word.Word))
                            {
                                if (!IdentifiersToIgnore.Any(x => x.IsMatch(word.Word)))
                                {
                                    IList<string> replaceFormats = IdentifierResolver.GetReplaceFormats(
                                        Options.Declaration, Options.Solution, word.Word,
                                        Options.IdentifierLookupScope);

                                    if (replaceFormats.Count == 1)
                                    {
                                        UnknownTagNode seeNode = new UnknownTagNode(Options);
                                        seeNode.StartTag = string.Format(replaceFormats[0], word.Word);
                                        seeNode.EndTag = "";
                                        currentLine.Add(seeNode);
                                        lineStart = false;
                                        continue;
                                    }
                                }

                            }

                            // Are we in a list?
                            if (list == null)
                            {
                                // Nope so just append the word to the current line
                                currentLine.Add(word);
                                lineStart = false;
                                continue;
                            }

                            // yep - in a list

                            // Are we in an item?
                            if (desc == null)
                            {
                                // no - so that's the end of the list
                                if (list.Items.Count > 0) newContents.Add(list);
                                list = null;
                                currentLine.Add(word);
                                lineStart = false;
                                continue;
                            }

                            // Yes in an item so append to that
                            desc.Contents.Add(word);

                        }
                        lineStart = false;
                        continue;
                    }

                    // Middle of a line, just append this word to whatever the current thing is

                    // But check if it's an identifier that needs replacing first.
                    if (IdentifierResolver.IsIdentifier(Options.Declaration, Options.Solution, word.Word, Options.IdentifierLookupScope) ||
                        IdentifierResolver.IsKeyword(Options.Declaration, Options.Solution, word.Word))
                    {
                        if (!IdentifiersToIgnore.Any(x => x.IsMatch(word.Word)))
                        {
                            IList<string> replaceFormats = IdentifierResolver.GetReplaceFormats(
                                Options.Declaration, Options.Solution, word.Word,
                                Options.IdentifierLookupScope);

                            if (replaceFormats.Count == 1)
                            {
                                UnknownTagNode seeNode = new UnknownTagNode(Options);
                                seeNode.StartTag = string.Format(replaceFormats[0], word.Word);
                                seeNode.EndTag = "";
                                currentLine.Add(seeNode);
                                lineStart = false;
                                continue;
                            }
                        }

                    }

                    if (desc == null)
                    {
                        // Just a regular paragraph
                        currentLine.Add(word);
                    }
                    else
                    {
                        // List item
                        desc.Contents.Add(word);
                    }
                    continue;
                }

                // not a word, should then be some other tag
                
                // ask it to insert missing tags
                xmlCommentNode.InsertMissingTags();

                // Now look at what we're currently adding
                if (list == null)
                {
                    // Regular paragraph.

                    // Is it an inline tag?
                    if (xmlCommentNode is ExampleNode ||
                        xmlCommentNode is ParaNode ||
                        xmlCommentNode is ListNode ||
                        xmlCommentNode is CodeNode)
                    {
                        // Not an inline tag so end the current paragraph
                        if (currentLine.Count > 0)
                        {
                            ParaNode para = new ParaNode(Options);
                            para.Contents = currentLine;
                            newContents.Add(para);
                        }

                        // And just add this node directly into the new contentts
                        newContents.Add(xmlCommentNode);

                        // And start a new line
                        currentLine = new List<IXmlCommentNode>();
                        lineStart = false;
                    } 
                    else
                    {
                        // Yep inline tag so just add to the current line
                        currentLine.Add(xmlCommentNode);
                        lineStart = false;
                    }
                }
                else
                {
                    // In a list
                    if (desc == null)
                    {
                        // not in an item so this ends the list
                        // no - so that's the end of the list
                        if (list.Items.Count > 0) newContents.Add(list);
                        list = null;
                        currentLine = new List<IXmlCommentNode>();
                        currentLine.Add(xmlCommentNode);
                        lineStart = false;
                    }
                    else
                    {
                        // In an item so just add to that item
                        desc.Contents.Add(xmlCommentNode);
                        lineStart = false;
                    }
                }
                
            }

            // Commit whatever the last object was
            if (list == null)
            {
                // Just a regular paragraph

                // Make sure there's actually some content
                if (currentLine.Count > 0)
                {
                    ParaNode para = new ParaNode(Options);
                    para.Contents = currentLine;
                    newContents.Add(para);
                }
            }
            else
            {
                // List in progress
   
                // Do we have an item?
                if (desc != null)
                {
                    // yep so close that
                    ItemNode item = new ItemNode(Options);
                    item.Description = desc;
                    list.Items.Add(item);
                }

                // add the list if it has items
                if (list.Items.Count > 0)
                {
                    newContents.Add(list);
                }
            }
            Contents = newContents;
        }


        private void AddTextNode(XmlText text)
        {
            List<string> lines =  new List<string>( text.InnerText.Split('\n'));

            foreach (string line in lines)
            {
                Contents.Add(new NewLineNode());

                string normalisedLine = _whitespaceRegex.Replace(line, " ").Trim();

                string[] words = normalisedLine.Split(' ');

                foreach (string word in words)
                {
                    // Ignore emtpy words
                    if (word == "") continue;

                    WordNode node = new WordNode {Word = word};
                    Contents.Add(node);
                }

            }
            if (lines[lines.Count - 1].Trim(new[] {' ', '\t'}).EndsWith("\n"))
            {
                // Append a last newline
                Contents.Add(new NewLineNode());
            }
        }

        public void AddNode(XmlNode node)
        {
            XmlElement element = node as XmlElement;
            if (element != null)
            {
                IXmlCommentNode childNode;
                switch (element.Name.ToLower())
                {
                    case "p":
                    case "para":
                        childNode = new ParaNode(Options);
                        break;
                    case "ul":
                        {
                            ListNode list = new ListNode(Options);
                            list.Type = ListTypes.Bullet;
                            childNode = list;
                            break;
                        }
                    case "ol":
                        {
                            ListNode list = new ListNode(Options);
                            list.Type = ListTypes.Number;
                            childNode = list;
                            break;
                        }
                    case "list":
                        childNode = new ListNode(Options);
                        break;
                    case "c":
                        childNode = new CNode(Options);
                        break;
                    case "code":
                        childNode = new CodeNode(Options);
                        break;
                    case "see":
                        childNode = new SeeNode(Options);
                        break;
                    case "seealso":
                        childNode = new SeeAlsoNode(Options);
                        break;
                    case "paramref":
                        childNode = new ParamRefNode(Options);
                        break;
                    case "typeparamref":
                        childNode = new TypeParamRefNode(Options);
                        break;
                    case "example":
                        childNode = new ExampleNode(Options);
                        break;
                    default:
                        // If we see another tag then they should have escaped it so do that for them
                        string outerXml = element.OuterXml;
                        string startTag = outerXml.Substring(0, outerXml.IndexOf('>') + 1).Trim();
                        //startTag = startTag.Replace("<", "&lt;").Replace(">", "&gt;");
                        string endTag = element.IsEmpty? "" : string.Format("</{0}>", element.LocalName);
                        childNode = new UnknownTagNode(Options) { StartTag = startTag, EndTag = endTag };
                        break;
                }
                childNode.FromXml(element);
                Contents.Add(childNode);

                return;
            }

            XmlText text = node as XmlText;
            if (text != null)
            {
                AddTextNode(text);
            }
        }

        public override void FromXml(XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                AddNode(child);
            }
        }

    }

    public class SummaryNode : ExtendedBlockNode
    {
        public SummaryNode(XmlCommentOptions options) : base(options) {}

        public override bool AllowLineCollapse
        {
            get { return false; }
        }

        public override string GetStartTag()
        {
            return "<summary>";
        }

        public override string GetEndTag()
        {
            return "</summary>";
        }
    }

    public class RemarksNode : ExtendedBlockNode
    {
        public RemarksNode(XmlCommentOptions options) : base(options) {}


        public override bool AllowLineCollapse
        {
            get { return false; }
        }

        public override string GetStartTag()
        {
            return "<remarks>";
        }

        public override string GetEndTag()
        {
            return "</remarks>";
        }
    }

    public class ValueNode : ExtendedBlockNode
    {
        public ValueNode(XmlCommentOptions options) : base(options) {}

        public override string GetStartTag()
        {
            return "<value>";
        }

        public override string GetEndTag()
        {
            return "</value>";
        }
    }

    public class ExampleNode : ExtendedBlockNode
    {

        public ExampleNode(XmlCommentOptions options) : base(options) {}

        public override bool AllowLineCollapse
        {
            get { return false; }
        }

        public override string GetStartTag()
        {
            return "<example>";
        }

        public override string GetEndTag()
        {
            return "</example>";
        }
    }
    public class ReturnsNode : ExtendedBlockNode
    {
        public ReturnsNode(XmlCommentOptions options) : base(options) {}

        public override string GetStartTag()
        {
            return "<returns>";
        }

        public override string GetEndTag()
        {
            return "</returns>";
        }
    }

    public class ParaNode : ExtendedBlockNode
    {
        public ParaNode(XmlCommentOptions options) : base(options) {}

        public bool NoTags = false;

        public override string GetStartTag()
        {
            if (NoTags) return "";
            return "<para>";
        }

        public override string GetEndTag()
        {
            if (NoTags) return "";
            return "</para>";
        }
    }

    public abstract class SeeNodeBase : ExtendedBlockNode
    {

        protected SeeNodeBase(XmlCommentOptions options) : base(options) {}

        [XmlAttribute("cref")]
        public string CRef { get; set; }

        public override void FromXml(XmlNode node)
        {
            XmlElement element = node as XmlElement;
            if (element == null) return;

            if (element.HasAttribute("cref", ""))
            {
                CRef = element.GetAttribute("cref", "");
            }

            base.FromXml(element);
        }

        public override void InsertMissingTags()
        {
            // See if we can shorten the CRef at all.
            CRef = IdentifierResolver.ContractCRef(CRef, Options.Declaration, Options.Solution,
                                            Options.IdentifierLookupScope);

            base.InsertMissingTags();
        }

        public abstract string GetSingleTag();

        public override string ToXml(int currentLineLength, int maxLineLength)
        {
            if (Contents.Count == 0)
            {
                return GetSingleTag();
            }
            return base.ToXml(currentLineLength, maxLineLength);
        }
    }

    public class ExceptionNode : SeeNodeBase
    {

        public ExceptionNode(XmlCommentOptions options) : base(options) {}

        public override string GetStartTag()
        {
            return string.Format("<exception cref=\"{0}\">", CRef);
        }

        public override string GetEndTag()
        {
            return "</exception>";
        }

        public override string GetSingleTag()
        {
            return string.Format("<exception cref=\"{0}\" />", CRef);
        }
    }
    public class PermissionNode : SeeNodeBase
    {
        public PermissionNode(XmlCommentOptions options) : base(options) {}


        public override string GetStartTag()
        {
            return string.Format("<permission cref=\"{0}\">", CRef);
        }

        public override string GetEndTag()
        {
            return "</permission>";
        }

        public override string GetSingleTag()
        {
            return string.Format("<permission cref=\"{0}\" />", CRef);
        }

    }
    public class SeeNode : SeeNodeBase
    {

        public SeeNode(XmlCommentOptions options) : base(options) {}

        [XmlAttribute("langword")]
        public string LangWord { get; set; }

        public override void FromXml(XmlNode node)
        {
            XmlElement element = node as XmlElement;
            if (element == null) return;

            if (element.HasAttribute("langword", ""))
            {
                LangWord = element.GetAttribute("langword", "");
            }

            base.FromXml(element);
        }

        public override string GetStartTag()
        {
            if (LangWord != null) return string.Format("<see langword=\"{0}\">", LangWord);
            return string.Format("<see cref=\"{0}\">", CRef);
        }

        public override string GetEndTag()
        {
            return "</see>";
        }

        public override string GetSingleTag()
        {
            if (LangWord != null) return string.Format("<see langword=\"{0}\" />", LangWord);
            return string.Format("<see cref=\"{0}\" />", CRef);
        }

    }
    public class SeeAlsoNode : SeeNodeBase
    {

        public SeeAlsoNode(XmlCommentOptions options) : base(options) {}

        public override string GetStartTag()
        {
            return string.Format("<seealso cref=\"{0}\">", CRef);
        }

        public override string GetEndTag()
        {
            return "</seealso>";
        }

        public override string GetSingleTag()
        {
            return string.Format("<seealso cref=\"{0}\" />", CRef);
        }

    }

    public class TermNode : ExtendedBlockNode
    {

        public TermNode(XmlCommentOptions options) : base(options) {}

        public override string GetStartTag()
        {
            return "<term>";
        }

        public override string GetEndTag()
        {
            return "</term>";
        }
    }

    public class DescriptionNode : ExtendedBlockNode
    {

        public DescriptionNode(XmlCommentOptions options) : base(options) {}

        public override string GetStartTag()
        {
            return "<description>";
        }

        public override string GetEndTag()
        {
            return "</description>";
        }
    }

    public abstract class ParamNodeBase : ExtendedBlockNode
    {

        protected ParamNodeBase(XmlCommentOptions options) : base(options) {}
    
        [XmlAttribute("name")]
        public string Name { get; set; }

        public override void FromXml(XmlNode node)
        {
            XmlElement element = node as XmlElement;
            if (element == null) return;

            if (element.HasAttribute("name", ""))
            {
                Name = element.GetAttribute("name", "");
            }

            base.FromXml(element);
        }

        public override IEnumerable<Regex> IdentifiersToIgnore
        {
            get
            {
                List<Regex> result = new List<Regex>() { new Regex("^" + Name + "$")};
                result.AddRange(base.IdentifiersToIgnore);
                return result;
            }
        }
    }

    public class ParamNode : ParamNodeBase
    {

        public ParamNode(XmlCommentOptions options) : base(options) {}

        public override string GetStartTag()
        {
            return string.Format("<param name=\"{0}\">", Name);
        }

        public override string GetEndTag()
        {
            return "</param>";
        }


    }
    public class TypeParamNode : ParamNodeBase
    {

        public TypeParamNode(XmlCommentOptions options) : base(options) {}

        public override string GetStartTag()
        {
            return string.Format("<typeparam name=\"{0}\">", Name);
        }

        public override string GetEndTag()
        {
            return "</typeparam>";
        }
    }

    public abstract class ParamRefNodeBase : XmlCommentNodeBase
    {

        protected ParamRefNodeBase(XmlCommentOptions options) : base(options) {}

        [XmlAttribute("name")]
        public string Name { get; set; }

        public override void InsertMissingTags()
        {
        }

        public override void FromXml(XmlNode node)
        {
            XmlElement element = node as XmlElement;
            if (element == null) return;

            if (element.HasAttribute("name", ""))
            {
                Name = element.GetAttribute("name", "");
            }
        }
    }

    public class ParamRefNode : ParamRefNodeBase
    {
        public ParamRefNode(XmlCommentOptions options) : base(options) {}

        public override string ToXml(int currentLineLength, int maxLineLength)
        {
            return string.Format("<paramref name=\"{0}\" />", Name);
        }
    }
    public class TypeParamRefNode : ParamRefNodeBase
    {

        public TypeParamRefNode(XmlCommentOptions options) : base(options) {}

        public override string ToXml(int currentLineLength, int maxLineLength)
        {
            return string.Format("<typeparamref name=\"{0}\" />", Name);
        }
    }


    public enum ListTypes
    {
        Bullet,
        Number,
        Table,
        None // Note this is the default - should never be used.
    }

    public class ListNode : XmlCommentNodeBase
    {
        public ListTypes Type { get; set; }

        public ListHeaderNode ListHeader { get; set; }

        public List<ItemNode> Items { get; set; }

        public ListNode(XmlCommentOptions options) : base (options)
        {
            Type = ListTypes.None;
            Items = new List<ItemNode>();
        }

        public override void InsertMissingTags()
        {
            if (ListHeader != null) ListHeader.InsertMissingTags();
            foreach (var item in Items) item.InsertMissingTags();
        }

        public override void FromXml(XmlNode node)
        {
            XmlElement element = node as XmlElement;
            if (element == null) return;

            if (Type == ListTypes.None)
            {
                Type = ListTypes.Bullet;
                if (element.HasAttribute("type", ""))
                {
                    string typeString = element.GetAttribute("type", "");
                    if (Enum.IsDefined(typeof(ListTypes), typeString))
                    {
                        Type = (ListTypes)Enum.Parse(typeof(ListTypes), typeString, true);
                    }
                }
            }

            foreach (XmlNode child in element.ChildNodes)
            {
                XmlElement childElement = child as XmlElement;
                if (childElement != null)
                {

                    switch (childElement.LocalName.ToLower())
                    {
                        case "listheader":
                            if (ListHeader != null) continue; // Ignore if we already have a header
                            ListHeaderNode listHeader = new ListHeaderNode(Options);
                            listHeader.FromXml(childElement);
                            ListHeader = listHeader;
                            break;
                        case "li":
                        case "item":
                            {
                                ItemNode item = new ItemNode(Options);
                                item.FromXml(childElement);
                                Items.Add(item);
                                break;
                            }
                        default:
                            {
                                // Just stick other tags inside an item
                                ItemNode item = new ItemNode(Options);
                                item.Description = new DescriptionNode(Options);
                                item.Description.AddNode(childElement);
                                Items.Add(item);
                            }
                            break;

                    }
                    continue;
                }

                // Everything else is bogus but add it as an item
                ItemNode fakeItem = new ItemNode(Options);
                fakeItem.Description = new DescriptionNode(Options);
                fakeItem.Description.AddNode(child);
                Items.Add(fakeItem);
            }
        }

        public override string ToXml(int currentLineLength, int maxLineLength)
        {
            StringBuilder sb = new StringBuilder();

            string type;
            switch (Type)
            {
                case ListTypes.Number:
                    type = "number";
                    break;
                case ListTypes.Table:
                    type = "table";
                    break;
                default:
                    type = "bullet";
                    break;
            }

            sb.Append(string.Format("<list type=\"{0}\">\n", type));
            if (ListHeader != null)
            {
                sb.Append(ListHeader.ToXml(0, maxLineLength));
                sb.Append("\n");
            }
            foreach (ItemNode itemNode in Items)
            {
                sb.Append(itemNode.ToXml(0, maxLineLength));
                sb.Append("\n");
            }
            sb.Append("</list>");
            return sb.ToString();
        }
    }

    public abstract class ItemNodeBase : XmlCommentNodeBase
    {
        protected ItemNodeBase(XmlCommentOptions options) : base(options) {}

        public TermNode Term { get; set; }

        public DescriptionNode Description { get; set; }

        public override void InsertMissingTags()
        {
            if (Term != null) Term.InsertMissingTags();
            if (Description != null) Description.InsertMissingTags();
        }

        public override void FromXml(XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                XmlElement childElement = child as XmlElement;
                if (childElement != null)
                {
                    switch (childElement.LocalName.ToLower())
                    {
                        case "term":
                            if (Term != null) continue; // Ignore if we already have a header
                            TermNode term = new TermNode(Options);
                            term.FromXml(childElement);
                            Term = term;
                            break;
                        case "description":
                            if (Description != null) continue; // Ignore if we already have a header
                            DescriptionNode desc = new DescriptionNode(Options);
                            desc.FromXml(childElement);
                            Description = desc;
                            break;
                    }
                    continue;
                }

                XmlText text = node as XmlText;
                if (text != null)
                {
                    if (Description == null) Description = new DescriptionNode(Options);
                    Description.AddNode(node);
                }
            }
        }

        public abstract string GetStartTag();
        public abstract string GetEndTag();

        public override string ToXml(int currentLineLength, int maxLineLength)
        {
            // Get the various text strings

            // Start tag
            string startTag = GetStartTag();

            // End tag
            string endTag = GetEndTag();

            // The term section
            string term = "";
            if (Term != null) term = Term.ToXml(0, maxLineLength);

            // The description section
            string desc = "";
            if (Description != null) desc = Description.ToXml(0, maxLineLength);

            // If any of the sections used a new line OR the whole thing is too long to fit on one line then...
            if (term.Contains("\n") ||
                desc.Contains("\n") ||
                currentLineLength + startTag.Length + term.Length + desc.Length + endTag.Length > maxLineLength )
            {
                StringBuilder sb1 = new StringBuilder();

                sb1.Append(startTag);
                if (term.Length != 0)
                {
                    sb1.Append("\n");
                    sb1.Append(term);
                }
                if (desc.Length != 0)
                {
                    sb1.Append("\n");
                    sb1.Append(desc);
                }
                sb1.Append("\n");
                sb1.Append(endTag);

                return sb1.ToString();
            }

            // Looks like it should all fit.
            return string.Format("{0}{1}{2}{3}", startTag, term, desc, endTag);
        }
    }

    public class ListHeaderNode : ItemNodeBase
    {

        public ListHeaderNode(XmlCommentOptions options) : base(options) {}

        public override string GetStartTag()
        {
            return "<listheader>";
        }

        public override string GetEndTag()
        {
            return "</listheader>";
        }
    }
    public class ItemNode : ItemNodeBase
    {

        public ItemNode(XmlCommentOptions options) : base(options) {}

        public override string GetStartTag()
        {
            return "<item>";
        }

        public override string GetEndTag()
        {
            return "</item>";
        }
    }

    public enum CodeLanguageTypes
    {
        CSharp,
        VB
    }

    public abstract class CodeNodeBase : XmlCommentNodeBase
    {
        private static readonly Regex _newlineSpace = new Regex("\n ");
        private static readonly Regex _newlineNewline = new Regex("\n\n");

        [XmlAttribute("lang")]
        public CodeLanguageTypes Lang { get; set; }

        [XmlText]
        public string Text { get; set; }

        protected CodeNodeBase(XmlCommentOptions options) : base(options)
        {
            Text = "";
        }

        public override void InsertMissingTags()
        {
        }

        public override void FromXml(XmlNode node)
        {
            XmlElement element = node as XmlElement;
            if (element == null) return;

            Lang = CodeLanguageTypes.CSharp;
            if (element.HasAttribute("lang", ""))
            {
                string langString = element.GetAttribute("lang", "");
                if (Enum.IsDefined(typeof(CodeLanguageTypes), langString))
                {
                    Lang = (CodeLanguageTypes)Enum.Parse(typeof(CodeLanguageTypes), langString, true);
                }
            }

            foreach (XmlNode child in node.ChildNodes)
            {
                XmlElement childElement = child as XmlElement;
                if (childElement != null)
                {
                    // If we see another tag then they should have escaped it so do that for them
                    string outerXml = childElement.OuterXml;
                    string startTag = outerXml.Substring(0, outerXml.IndexOf('>') - 1).Trim();
                    startTag = startTag.Replace("<", "&lt;").Replace(">", "&gt;");
                    string endTag = childElement.IsEmpty? "" : string.Format("&lt;/{0}&gt;", childElement.LocalName);
                    string innerXml = childElement.InnerXml.Replace("<", "&lt;").Replace(">", "&gt;");
                    Text += startTag + innerXml + endTag;
                    continue;
                }

                XmlText text = child as XmlText;
                if (text != null)
                {
                    if (Text.Length != 0) Text += " ";

                    string txt = text.InnerText;
                    txt = txt.Replace("<", "&lt;").Replace(">", "&gt;");
                    if (txt.Count(x => x == '\n') == _newlineSpace.Matches(txt).Count + _newlineNewline.Matches(txt).Count)
                    {
                        // all new lines start with a space so assume it must have been the indent for the whole comment and strip it.
                        txt = _newlineSpace.Replace(txt, "\n");
                    }

                    // Stupid xml reader doesn't strip the starting space off each line so do that.

                    Text += txt;
                }
            }
        }

        public abstract string GetStartTag();
        public abstract string GetEndTag();

        public override string ToXml(int currentLineLength, int maxLineLength)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetStartTag());
            sb.Append(Text);
            sb.Append(GetEndTag());
            string result = sb.ToString();

            if (result.Contains("\n"))
            {
                // There are new lines.

                // Get the length of the first line
                int firstLineLength = result.IndexOf("\n");
                
                if (currentLineLength + firstLineLength > maxLineLength)
                {
                    return "\n" + result;
                }

                return result;
            }

            // No new lines
            if (currentLineLength + result.Length > maxLineLength)
            {
                return "\n" + result;
            }
            return result;
        }
    }

    public class CodeNode : CodeNodeBase
    {

        public CodeNode(XmlCommentOptions options) : base(options) {}

        public override string GetStartTag()
        {
            return "<code>";
        }

        public override string GetEndTag()
        {
            return "</code>";
        }
    }

    public class CNode : CodeNodeBase
    {

        public CNode(XmlCommentOptions options) : base(options) {}

        public override string GetStartTag()
        {
            return "<c>";
        }

        public override string GetEndTag()
        {
            return "</c>";
        }
    }

    public class UnknownTagNode : ExtendedBlockNode
    {

        public UnknownTagNode(XmlCommentOptions options) : base(options) {}

        public string StartTag { get; set; }
        public string EndTag { get; set; }

        public override string GetStartTag()
        {
            return StartTag;
        }

        public override string GetEndTag()
        {
            return EndTag;
        }
    }

    public class WordNode : IXmlCommentNode
    {
        public string Word { get; set; }

        public void InsertMissingTags()
        {
        }

        public string ToXml(int currentLineLength, int maxLineLength)
        {
            return Word;
        }

        public void FromXml(XmlNode node)
        {
        }

        public override string ToString()
        {
            return Word;
        }
    }

    public class NewLineNode : IXmlCommentNode
    {
        public void InsertMissingTags()
        {
        }

        public string ToXml(int currentLineLength, int maxLineLength)
        {
            return "\n";
        }

        public void FromXml(XmlNode node)
        {
        }
    }
}
