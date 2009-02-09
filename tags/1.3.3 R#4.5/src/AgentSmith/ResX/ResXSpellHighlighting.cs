using System;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;

namespace AgentSmith.ResX
{
    [ConfigurableSeverityHighlighting(NAME)]
    public class ResXSpellHighlighting : SpellCheckSuggestionBase
    {
        public const string NAME = "ResxSpellCheckSuggestion";
        private readonly IProjectFile _file;

        public ResXSpellHighlighting(string word, IProjectFile file, ISpellChecker spellChecker, DocumentRange range)
            : base(NAME, range, word, file.GetSolution(), spellChecker)
        {
            _file = file;
        }

        public IProjectFile File
        {
            get { return _file; }
        }
    }
}