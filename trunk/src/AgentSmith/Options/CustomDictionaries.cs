using System;
using System.Runtime.CompilerServices;
using AgentSmith.SpellCheck.NetSpell;

namespace AgentSmith.Options
{
    [Serializable]
    public class CustomDictionaries
    {
        private CustomDictionary[] _customDictionaries = new CustomDictionary[0];

        public CustomDictionary[] Dictionaries
        {
            get { return _customDictionaries; }
            set { _customDictionaries = value; }
        }

        private CustomDictionary getCustomDictionary(string name)
        {
            string lowerName = name.ToLower();
            foreach (CustomDictionary dictionary in Dictionaries)
            {
                if (dictionary.Name.ToLower() == lowerName)
                {
                    return dictionary;
                }
            }

            return null;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public CustomDictionary GetOrCreateCustomDictionary(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Invalid dictionary name.");
            }
            CustomDictionary dictionary = getCustomDictionary(name);
            if (dictionary != null)
            {
                return dictionary;
            }

            dictionary = new CustomDictionary();
            dictionary.Name = name;
            Array.Resize(ref _customDictionaries, _customDictionaries.Length + 1);
            _customDictionaries[_customDictionaries.Length - 1] = dictionary;
            return dictionary;
        }
    }
}