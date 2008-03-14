using System;

namespace AgentSmith.SpellCheck.NetSpell
{
    [Serializable]
    public class CustomDictionary
    {
        private string _name;
        private string _userWords = "";

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string UserWords
        {
            get { return _userWords; }
            set { _userWords = value; }
        }
    }
}