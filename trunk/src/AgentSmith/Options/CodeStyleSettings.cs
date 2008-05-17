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
        
        private CustomDictionaries _customDictionaries = new CustomDictionaries();
        private string _stringsDictionary = "en-US";        
        private string _identifierDictionary = "en-US";
        private string _defaultResXDictionary = "en-US";
        private string _lastSelectedCustomDictionary = "en-US";

        private Match[] _identifiersToSpellCheck;
        private Match[] _identifiersNotToSpellCheck;

        public CodeStyleSettings()
        {
            _namingConventionSettings.LoadDefaults();
            _commentsSettings = new CommentsSettings();
            _commentsSettings.CommentMatch = new Match[] { new Match(Declaration.Any, AccessLevels.Public | AccessLevels.Protected | AccessLevels.ProtectedInternal), };
            _identifiersToSpellCheck = new Match[] { new Match(Declaration.Any, AccessLevels.Public | AccessLevels.Protected | AccessLevels.ProtectedInternal) };
        }


        public CustomDictionaries CustomDictionaries
        {
            get { return _customDictionaries; }
            set { _customDictionaries = value; }
        }

        public string StringsDictionary
        {
            get { return _stringsDictionary; }
            set { _stringsDictionary = value; }
        }

        public string IdentifierDictionary
        {
            get { return _identifierDictionary; }
            set { _identifierDictionary = value; }
        }

        public string LastSelectedCustomDictionary
        {
            get { return _lastSelectedCustomDictionary; }
            set { _lastSelectedCustomDictionary = value; }
        }

        public string DefaultResXDictionary
        {
            get { return _defaultResXDictionary; }
            set { _defaultResXDictionary = value; }
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

        public Match[] IdentifiersToSpellCheck
        {
            get { return _identifiersToSpellCheck; }
            set { _identifiersToSpellCheck = value; }
        }

        public Match[] IdentifiersNotToSpellCheck
        {
            get { return _identifiersNotToSpellCheck; }
            set { _identifiersNotToSpellCheck = value; }
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
                    _customDictionaries = settings._customDictionaries;
                    _stringsDictionary = settings._stringsDictionary;
                    _defaultResXDictionary = settings._defaultResXDictionary;
                    _identifierDictionary = settings._identifierDictionary;
                    _lastSelectedCustomDictionary = settings._lastSelectedCustomDictionary;
                    _identifiersToSpellCheck = settings._identifiersToSpellCheck;
                    _identifiersNotToSpellCheck = settings._identifiersNotToSpellCheck;
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