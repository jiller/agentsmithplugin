using System;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Editor;

namespace AgentSmith.SpellCheck
{        
    public abstract class SpellCheckSuggestionBase : SuggestionBase
    {
        private readonly string _word;
        private readonly ISolution _solution;
        private readonly ISpellChecker _spellChecker;

        public SpellCheckSuggestionBase(string suggestionName, DocumentRange range, string misspelledWord,
            ISolution solution, ISpellChecker spellChecker)
            : base(suggestionName, null, range, String.Format("Word '{0}' is not in dictionary.", misspelledWord))
        {
            _word = misspelledWord;
            _solution = solution;
            _spellChecker = spellChecker;
        }

        public string MisspelledWord
        {
            get { return _word; }
        }

        public ISolution Solution
        {
            get { return _solution; }
        }

        public ISpellChecker SpellChecker
        {
            get { return _spellChecker; }
        }
    }
}