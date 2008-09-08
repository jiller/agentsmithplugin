using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using AgentSmith.NamingConventions;

namespace AgentSmith.Options
{
    [Serializable]
    public class NamingConventionSettings
    {
        private NamingConventionRule[] _rules;
        private string[] _exclusions = new string[0];

        public NamingConventionRule[] Rules
        {
            get { return _rules; }
            set { _rules = value; }
        }

        public void LoadDefaults()
        {
            XmlSerializer serializer = new XmlSerializer(GetType());
            using (
                Stream stream =
                    Assembly.GetAssembly(GetType()).GetManifestResourceStream(GetType(),
                                                                              "DefaultNamingConventionSettings.xml"))
            {
                XmlReader reader = XmlReader.Create(stream);
                NamingConventionSettings settings = (NamingConventionSettings) serializer.Deserialize(reader);
                _rules = settings.Rules;
                _exclusions = settings.Exclusions;
            }
        }

        public string[] Exclusions
        {
            get { return _exclusions; }
            set { _exclusions = value ?? new string[0]; }
        }
    }
}