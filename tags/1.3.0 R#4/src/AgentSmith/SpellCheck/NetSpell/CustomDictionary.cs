using System;
using System.Text;
using System.Xml.Serialization;

namespace AgentSmith.SpellCheck.NetSpell
{
    [Serializable]
    public class CustomDictionary
    {
        private bool _caseSensitive;
        private string _name;
        private string _userWords = "";
        private string _decodedWords = null;
        private int _version;
        private bool _encoded;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                _version++;
            }
        }

        public bool Encoded
        {
            get { return _encoded; }
            set { _encoded = value; }
        }

        public bool CaseSensitive
        {
            get { return _caseSensitive; }
            set
            {
                _caseSensitive = value;
                _version++;
            }
        }

        /// <summary>
        /// Encoded string. Encoding because in R# 3.1 non ASCII characters may cause problems.
        /// </summary>
        public string UserWords
        {
            get
            {
                return _decodedWords == null ? "" : Convert.ToBase64String(Encoding.Unicode.GetBytes(_decodedWords));
            }
            set
            {
                _userWords = value;
                _decodedWords = null;
                _version++;
            }
        }

        public int Version
        {
            get { return _version; }
        }

        [XmlIgnore]
        public string DecodedUserWords
        {
            get
            {
                if (_decodedWords == null)
                {
                    _decodedWords = _encoded ? Encoding.Unicode.GetString(Convert.FromBase64String(_userWords)) : _userWords;
                }

                return _decodedWords;
            }
            set
            {
                _encoded = true;
                _decodedWords = value;
                _version++;
            }
        }        
    }
}