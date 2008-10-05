using System;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;

namespace AgentSmith.SpellCheck
{        
    /// <summary>
    /// Base class for all spell check suggestions.
    /// </summary>
    public abstract class SpellCheckSuggestionBase : SuggestionBase
    {
        private readonly string _word;
        private readonly ISolution _solution;
        private readonly ISpellChecker _spellChecker;

        /// <summary>
        /// Instantiates new instance.
        /// </summary>
        /// <param name="suggestionName">
        /// Name of suggestion as appears in <see cref="ConfigurableSeverityHighlightingAttribute"/>
        /// attribute.</param>
        /// <param name="range">Range which misspelled word occupies.</param>
        /// <param name="misspelledWord">Word that is misspelled.</param>
        /// <param name="solution"><see cref="ISolution"/> instance.</param>
        /// <param name="spellChecker">Spell checker instance.</param>
        protected SpellCheckSuggestionBase(string suggestionName, DocumentRange range, string misspelledWord,
            ISolution solution, ISpellChecker spellChecker)
            : base(suggestionName, null, range, String.Format("Word '{0}' is not in dictionary.", misspelledWord))
        {
            _word = misspelledWord;
            _solution = solution;
            _spellChecker = spellChecker;
        }

        /// <summary>
        /// Word that was misspelled.
        /// </summary>
        public string MisspelledWord
        {
            get { return _word; }
        }

        /// <summary>
        /// Solution instance.
        /// </summary>
        public ISolution Solution
        {
            get { return _solution; }
        }

        /// <summary>
        /// Spell checked instance.
        /// </summary>
        public ISpellChecker SpellChecker
        {
            get { return _spellChecker; }
        }
    }
}