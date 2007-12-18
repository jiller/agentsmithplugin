using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using AgentSmith.Comments;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp.Stages;
using JetBrains.ReSharper.Daemon.Impl;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace AgentSmith
{
    [DaemonStage(StagesBefore = new Type[] { typeof(GlobalErrorStage) },
        StagesAfter = new Type[] { typeof(LanguageSpecificDaemonStage) }, RunForInvisibleDocument = true)]
    public class ResXDaemonStage : IDaemonStage
    {
        public IDaemonStageProcess CreateProcess(IDaemonProcess process)
        {
            if (process.ProjectFile.Name.EndsWith(".resx"))
            {
                return new ResXProcess(process.ProjectFile, this);
            }

            return null;
        }

        public ErrorStripeRequest NeedsErrorStripe(IProjectFile projectFile)
        {
            return ErrorStripeRequest.STRIPE_AND_ERRORS;
        }
    }

    internal class ResXProcess : DaemonProcessBase, IDaemonStageProcess
    {
        private readonly IProjectFile _file;

        public ResXProcess(IProjectFile file, ResXDaemonStage stage)
            : base(file, new IDaemonStage[] { stage })
        {
            _file = file;            
        }

        public override bool IsRangeInvalidated(TextRange range)
        {
            throw new NotImplementedException();
        }

        public override bool HasInvalidatedRangeOutside(ICollection<TextRange> ranges)
        {
            throw new NotImplementedException();
        }

        public override void CommitHighlighters(CommitContext context)
        {
            throw new NotImplementedException();
        }

        public override bool FullRehighlightingRequired
        {
            get { throw new NotImplementedException(); }
        }
        
        public DaemonStageProcessResult Execute()
        {
            IXmlFile xmlFile = PsiManager.GetInstance(_file.GetSolution()).GetPsiFile(_file) as IXmlFile;
            if (xmlFile != null)
            {
                IXmlTag root = xmlFile.GetTag(delegate(IXmlTag tag)
                {
                    return tag.TagName == "root";
                });

                if (root != null)
                {
                    IList<IXmlTag> datas = root.GetTags(delegate(IXmlTag tag) { return tag.TagName == "data"; });
                    foreach (IXmlTag data in datas)
                    {
                        if (data.GetAttribute("type") == null)
                        {
                            IXmlTag val = data.GetTag(delegate(IXmlTag tag) { return tag.TagName == "value"; });
                            if (val != null)
                            {
                                Logger.LogMessage("ggg" + val.GetText());
                            }
                        }
                    }
                }                
            }

            DaemonStageProcessResult result = new DaemonStageProcessResult();
            return result;          
        }

        
        /*internal class XmlElement
        {
            public readonly string Name;
            public readonly Dictionary<string, string> _attributes = new Dictionary<string, string>();
            public readonly ITextRange Range;
        }
        
        public interface IXmlReader
        {
            
        }*/
    }
}