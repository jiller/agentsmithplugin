using System.Collections.Generic;
using System.Text.RegularExpressions;

using AgentSmith.SpellCheck.NetSpell;

using JetBrains.Application.Settings;
using JetBrains.Application.Settings.Store;
using JetBrains.ReSharper.Psi.Caches;
using JetBrains.ReSharper.Settings;

namespace AgentSmith.Options
{
    [SettingsKey(typeof(CodeStyleSettings), "Agent Smith Settings")]
    public class AgentSmithSettings
    {


    }


    [SettingsKey(typeof(AgentSmithSettings), "Xml Documentation Settings")]
    public class XmlDocumentationSettings
    {
        private bool _wordsToIgnoreChanged = true;

        private string _wordsToIgnore;

        private List<Regex> _cachedWordsToIgnore;

        private bool _wordsToIgnoreForMetataggingChanged = true;

        private string _wordsToIgnoreForMetatagging;

        private List<Regex> _cachedWordsToIgnoreForMetatagging;
        

        [SettingsEntry("en-US", "The dictionary/s to use for xml documentation comments in source code files (use commas to separate dictionary names)")]
        public string DictionaryName { get; set; }

        public string[] DictionaryNames
        {
            get { return DictionaryName.Split(','); }
        }

        [SettingsEntry(true, "Suppress missing comment tests if identifier inherits documentation?")]
        public bool SuppressIfBaseHasComment { get; set; }

        [SettingsEntry(80, "The maximum number of characters to allow on a line when reflowing xml documentation")]
        public int MaxCharactersPerLine { get; set; }

        [SettingsEntry("", "Regular expressions for words to ignore (separate with new lines)")]
        public string WordsToIgnore
        {
            get { return _wordsToIgnore; }
            set
            {
                _wordsToIgnoreChanged = true;
                _wordsToIgnore = value;
            }
        }

        public List<Regex> CompiledWordsToIgnore
        {
            get
            {
                if (_wordsToIgnoreChanged)
                {
                    _cachedWordsToIgnore = new List<Regex>();
                    string[] regexPatterns = _wordsToIgnore.Replace("\r", "").Split('\n');

                    foreach (string regexPattern in regexPatterns)
                    {
                        if (string.IsNullOrEmpty(regexPattern)) continue;
                        Regex re = new Regex(regexPattern);
                        _cachedWordsToIgnore.Add(re);
                    }
                    _wordsToIgnoreChanged = false;

                }
                return _cachedWordsToIgnore;
            }
        }

        [SettingsEntry("", "Regular expressions for identifiers to ignore as for metatags (separate with new lines)")]
        public string WordsToIgnoreForMetatagging
        {
            get { return _wordsToIgnoreForMetatagging; }
            set
            {
                _wordsToIgnoreForMetataggingChanged = true;
                _wordsToIgnoreForMetatagging = value;
            }
        }

        public List<Regex> CompiledWordsToIgnoreForMetatagging
        {
            get
            {
                if (_wordsToIgnoreForMetataggingChanged)
                {
                    _cachedWordsToIgnoreForMetatagging = new List<Regex>();
                    string[] regexPatterns = _wordsToIgnoreForMetatagging.Replace("\r", "").Split('\n');

                    foreach (string regexPattern in regexPatterns)
                    {
                        if (string.IsNullOrEmpty(regexPattern)) continue;
                        Regex re = new Regex(regexPattern);
                        _cachedWordsToIgnoreForMetatagging.Add(re);
                    }
                    _wordsToIgnoreForMetataggingChanged = false;
                }
                return this._cachedWordsToIgnoreForMetatagging;
            }
        }

    }

    public enum IdentifierLookupScopes
    {
        ProjectOnly,
        ProjectAndUsings,
        ProjectAndReferencedLibraries,
        ProjectAndAllLibraries
    }

    public static class IdentifierLookupScopesEx
    {
        public static DeclarationCacheLibraryScope AsLibraryScope(this IdentifierLookupScopes scope)
        {
            switch (scope)
            {
                case IdentifierLookupScopes.ProjectOnly:
                    return DeclarationCacheLibraryScope.NONE;
                case IdentifierLookupScopes.ProjectAndUsings:
                    return DeclarationCacheLibraryScope.TRANSITIVE;
                case IdentifierLookupScopes.ProjectAndReferencedLibraries:
                    return DeclarationCacheLibraryScope.REFERENCED;
                case IdentifierLookupScopes.ProjectAndAllLibraries:
                    return DeclarationCacheLibraryScope.FULL;
            }
            return DeclarationCacheLibraryScope.NONE;
        }
    }

    [SettingsKey(typeof(AgentSmithSettings), "Identifier Settings")]
    public class IdentifierSettings
    {
        private bool _wordsToIgnoreChanged = true;

        private string _wordsToIgnore;

        private List<Regex> _cachedWordsToIgnore;

        [SettingsEntry("en-US", "The dictionary/s to use for identifiers in source code files (use commas to separate dictionary names)")]
        public string DictionaryName { get; set; }

        public string[] DictionaryNames
        {
            get { return DictionaryName.Split(','); }
        }

        [SettingsEntry(1, "Scope for searching for identifiers")]
        public int LookupScope { get; set; }

        public IdentifierLookupScopes IdentifierLookupScope
        {
            get
            {
                switch (LookupScope)
                {
                    case 0:
                        return IdentifierLookupScopes.ProjectOnly;
                    case 1:
                        return IdentifierLookupScopes.ProjectAndUsings;
                    case 2:
                        return IdentifierLookupScopes.ProjectAndReferencedLibraries;
                    case 3:
                        return IdentifierLookupScopes.ProjectAndAllLibraries;
                }
                return IdentifierLookupScopes.ProjectAndUsings;
            }
        }

        [SettingsEntry("", "Regular expressions for identifiers to ignore during spell checking (separate with new lines)")]
        public string WordsToIgnore
        {
            get { return this._wordsToIgnore; }
            set
            {
                this._wordsToIgnoreChanged = true;
                this._wordsToIgnore = value;
            }
        }

        public List<Regex> CompiledWordsToIgnore
        {
            get
            {
                if (_wordsToIgnoreChanged)
                {
                    _cachedWordsToIgnore = new List<Regex>();
                    string[] regexPatterns = _wordsToIgnore.Replace("\r", "").Split('\n');

                    foreach (string regexPattern in regexPatterns)
                    {
                        if (string.IsNullOrEmpty(regexPattern)) continue;
                        Regex re = new Regex(regexPattern);
                        _cachedWordsToIgnore.Add(re);
                    }
                    _wordsToIgnoreChanged = false;
                }
                return _cachedWordsToIgnore;
            }
        }


    }

    [SettingsKey(typeof(AgentSmithSettings), "String Literal Settings")]
    public class StringSettings
    {
        private bool _wordsToIgnoreChanged = true;

        private string _wordsToIgnore;

        private List<Regex> _cachedWordsToIgnore;

        [SettingsEntry("en-US", "The dictionary/s to use for string literals in source code files (use commas to separate dictionary names)")]
        public string DictionaryName { get; set; }

        public string[] DictionaryNames
        {
            get { return DictionaryName.Split(','); }
        }

        [SettingsEntry(true, "Spell check verbatim strings?")]
        public bool IgnoreVerbatimStrings { get; set; }

        [SettingsEntry("", "Regular expressions for words to ignore (separate with new lines)")]
        public string WordsToIgnore
        {
            get { return _wordsToIgnore; }
            set
            {
                _wordsToIgnoreChanged = true;
                _wordsToIgnore = value;
            }
        }

        public List<Regex> CompiledWordsToIgnore
        {
            get
            {
                if (_wordsToIgnoreChanged)
                {
                    _cachedWordsToIgnore = new List<Regex>();
                    string[] regexPatterns = _wordsToIgnore.Replace("\r", "").Split('\n');

                    foreach (string regexPattern in regexPatterns)
                    {
                        if (string.IsNullOrEmpty(regexPattern)) continue;
                        Regex re = new Regex(regexPattern);
                        _cachedWordsToIgnore.Add(re);
                    }
                    _wordsToIgnoreChanged = false;
                }
                return _cachedWordsToIgnore;
            }
        }
    }

    [SettingsKey(typeof(AgentSmithSettings), "Resource File Settings")]
    public class ResXSettings
    {
        private bool _wordsToIgnoreChanged = true;

        private string _wordsToIgnore;

        private List<Regex> _cachedWordsToIgnore;

        [SettingsEntry("en-US", "The additional (to the resX language) dictionary/s to use for string literals in source code files (use commas to separate dictionary names)")]
        public string DictionaryName { get; set; }

        public string[] DictionaryNames
        {
            get { return DictionaryName.Split(','); }
        }

        [SettingsEntry("", "Regular expressions for words to ignore (separate with new lines)")]
        public string WordsToIgnore
        {
            get { return _wordsToIgnore; }
            set
            {
                _wordsToIgnoreChanged = true;
                _wordsToIgnore = value;
            }
        }

        public List<Regex> CompiledWordsToIgnore
        {
            get
            {
                if (_wordsToIgnoreChanged)
                {
                    _cachedWordsToIgnore = new List<Regex>();
                    string[] regexPatterns = _wordsToIgnore.Replace("\r", "").Split('\n');
                    
                    foreach (string regexPattern in regexPatterns)
                    {
                        if (string.IsNullOrEmpty(regexPattern)) continue;

                        Regex re = new Regex(regexPattern);
                        _cachedWordsToIgnore.Add(re);
                    }
                    _wordsToIgnoreChanged = false;
                }
                return _cachedWordsToIgnore;
            }
        }
    }


    [SettingsKey(typeof(AgentSmithSettings), "User Dictionaries")]
    public class CustomDictionarySettings
    {
        [SettingsIndexedEntry("User Dictionaries")]
        public IIndexedEntry<string, CustomDictionary> CustomDictionaries { get; set; }

    }

}


