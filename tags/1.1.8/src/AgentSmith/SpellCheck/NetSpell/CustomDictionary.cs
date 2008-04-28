using System;

namespace AgentSmith.SpellCheck.NetSpell
{
    [Serializable]
    public class CustomDictionary
    {
        private bool _caseSensitive;
        private string _name;
        private string _userWords = "";
        private int _version;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                _version++;
            }
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

        public string UserWords
        {
            get { return _userWords; }
            set
            {
                _userWords = value;
                _version++;
            }
        }

        public int Version
        {
            get { return _version; }
        }
    }
}