using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using AgentSmith.MemberMatch;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CodeStyle;
using JetBrains.Shell;
using JetBrains.Util;

namespace AgentSmith.Options
{
    [Serializable]
    [CodeStyleSettings("AgentSmithCSharpStyleSettings")]
    public class CodeStyleSettings : IXmlExternalizable, ICloneable
    {
        private NamingConventionSettings _namingConventionSettings = new NamingConventionSettings();
        private CommentsSettings _commentsSettings = new CommentsSettings();

        public CodeStyleSettings()
        {
            _namingConventionSettings.LoadDefaults();
            _commentsSettings = new CommentsSettings();
            _commentsSettings.CommentMatch = new Match[] { new Match(Declaration.Any, AccessLevels.Public) };
        }

        public CommentsSettings CommentsSettings
        {
            get { return _commentsSettings; }
            set { _commentsSettings = value; }
        }

        public NamingConventionSettings NamingConventionSettings
        {
            get { return _namingConventionSettings; }
            set { _namingConventionSettings = value; }
        }

        public static CodeStyleSettings GetInstance(ISolution solution)
        {
            JetBrains.ReSharper.Psi.CodeStyle.CodeStyleSettings settings = Shell.Instance.IsTestShell ? CodeStyleSettingsManager.Instance.CodeStyleSettings : SolutionCodeStyleSettings.GetInstance(solution).CodeStyleSettings;
            return settings.Get<CodeStyleSettings>();
        }

        #region IXmlExternalizable implementation

        #region ICloneable Members

        public object Clone()
        {
            XmlDocument document = new XmlDocument();
            XmlElement element = document.CreateElement("ID");
            WriteToXml(element);
            document.AppendChild(element);
            CodeStyleSettings settings = new CodeStyleSettings();
            settings.ReadFromXml(element);
            return settings;
        }

        #endregion

        #region IXmlExternalizable Members

        public void ReadFromXml(XmlElement element)
        {
            if (element != null)
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(GetType());
                    XmlReader reader = XmlReader.Create(new StringReader(element.InnerXml));
                    CodeStyleSettings settings = (CodeStyleSettings)serializer.Deserialize(reader);
                    _namingConventionSettings = settings.NamingConventionSettings;
                    _commentsSettings = settings.CommentsSettings;                    
                }
                catch (Exception ex)
                {
                    Logger.LogException("Failed to load Agent Smith settings", ex);
                }
            }
        }

        public bool WriteToXml(XmlElement element)
        {
            element.SetAttribute("version", "0");
            XmlSerializer serializer = new XmlSerializer(GetType());

            StringWriter sWriter = new StringWriter();
            XmlWriter writer = XmlWriter.Create(sWriter);

            serializer.Serialize(writer, this);
            XmlDocument document = new XmlDocument();
            document.LoadXml(sWriter.GetStringBuilder().ToString());
            element.InnerXml = document.DocumentElement.OuterXml;
            return true;
        }

        #endregion

        #endregion
    }
}