using System;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;

namespace AgentSmith.SpellCheck
{        
    public abstract class SpellCheckSuggestionBase : SuggestionBase
    {
        private readonly string _word;
        private readonly ISolution _solution;
        private readonly CustomDictionary _customDictionary;

        public SpellCheckSuggestionBase(string suggestionName, DocumentRange range, string misspelledWord,
            ISolution solution, CustomDictionary customDictionary)
            : base(suggestionName, null, range, String.Format("Word '{0}' is not in dictionary.", misspelledWord))
        {
            _word = misspelledWord;
            _solution = solution;
            _customDictionary = customDictionary;
        }

        public string MisspelledWord
        {
            get { return _word; }
        }

        public ISolution Solution
        {
            get { return _solution; }
        }

        public CustomDictionary CustomDictionary
        {
            get { return _customDictionary; }
        }
    }
}