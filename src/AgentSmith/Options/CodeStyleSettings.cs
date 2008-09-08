using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using AgentSmith.MemberMatch;
using JetBrains.Application;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CodeStyle;
using JetBrains.Util;
using Match=AgentSmith.MemberMatch.Match;

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
        private string _lastSelectedCustomDictionary = "en-US";

        private Match[] _identifiersToSpellCheck;
        private Match[] _identifiersNotToSpellCheck;
        private bool _isJustImported;
        private string[] _patternsToIgnore;
        private List<Regex> _compiledPatternsToIgnore;
        private static string[] DEFAULT_PATTERNS_TO_IGNORE = new string[]
                                    {
                                        @"(?#email)\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
                                        @"(?#url)http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"
                                    };

        private int CURRENT_VERSION = 1;

        public CodeStyleSettings()
        {
            _namingConventionSettings.LoadDefaults();
            _commentsSettings = new CommentsSettings();
            _commentsSettings.CommentMatch = new Match[] { new Match(Declaration.Any, AccessLevels.Public | AccessLevels.Protected | AccessLevels.ProtectedInternal), };
            _identifiersToSpellCheck = new Match[] { new Match(Declaration.Any, AccessLevels.Public | AccessLevels.Protected | AccessLevels.ProtectedInternal) };
            _patternsToIgnore = DEFAULT_PATTERNS_TO_IGNORE;
            _isJustImported = true;
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
        
        [XmlIgnore]
        public bool IsJustImported
        {
            get { return _isJustImported; }
            set { _isJustImported = value;}
        }

        [XmlIgnore]
        public List<Regex> CompiledPatternsToIgnore
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                if (_compiledPatternsToIgnore == null)
                {
                    _compiledPatternsToIgnore = new List<Regex>();
                    if (_patternsToIgnore != null)
                    {
                        foreach (string pattern in _patternsToIgnore)
                        {
                            try
                            {
                                _compiledPatternsToIgnore.Add(new Regex(pattern, RegexOptions.Compiled));
                            }
                            catch (Exception)
                            {
                                Logger.LogError("Incorrect regex: {0}", pattern);
                            }
                        }
                    }
                }
                return _compiledPatternsToIgnore;
            }
        }

        public string[] PatternsToIgnore
        {
            get { return _patternsToIgnore; }
            set
            {
                _patternsToIgnore = value;
                _compiledPatternsToIgnore = null;                
            }
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
                    convertToCurrentVersion(element);
                    XmlReader reader = XmlReader.Create(new StringReader(element.InnerXml));
                    CodeStyleSettings settings = (CodeStyleSettings)serializer.Deserialize(reader);
                    _namingConventionSettings = settings.NamingConventionSettings;
                    _commentsSettings = settings.CommentsSettings;
                    _customDictionaries = settings._customDictionaries;
                    _stringsDictionary = settings._stringsDictionary;
                    _identifierDictionary = settings._identifierDictionary;                    
                    _lastSelectedCustomDictionary = settings._lastSelectedCustomDictionary;
                    _identifiersToSpellCheck = settings._identifiersToSpellCheck;
                    _identifiersNotToSpellCheck = settings._identifiersNotToSpellCheck;
                    _patternsToIgnore = settings._patternsToIgnore;
                    _isJustImported = false;
                }
                catch (Exception ex)
                {
                    Logger.LogException("Failed to load Agent Smith settings", ex);
                }
            }
        }

        private void convertToCurrentVersion(XmlElement element)
        {
            int version = int.Parse(element.GetAttribute("version"));
            if(version <=0)
            {
                updateFrom0To1(element);
            }            
        }

        private void updateFrom0To1(XmlElement element)
        {
            element.SetAttribute("version", "1");
            XmlElement settings = (XmlElement)element.GetElementsByTagName("CodeStyleSettings")[0];
            
            XmlElement patterns = settings.OwnerDocument.CreateElement("PatternsToIgnore");
            settings.AppendChild(patterns);
            foreach (string patternText in DEFAULT_PATTERNS_TO_IGNORE)
            {
                XmlElement pattern = patterns.OwnerDocument.CreateElement("string");
                pattern.InnerText = patternText;
                patterns.AppendChild(pattern);
            }
        }

        public bool WriteToXml(XmlElement element)
        {
            element.SetAttribute("version", CURRENT_VERSION.ToString());
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