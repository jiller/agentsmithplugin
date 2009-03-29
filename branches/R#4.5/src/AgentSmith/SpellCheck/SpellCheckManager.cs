using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using AgentSmith.Options;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.ProjectModel;
using JetBrains.Util;

namespace AgentSmith.SpellCheck
{
    /// <summary>
    /// Manages spell checkers for different languages.
    /// Solution can be configured to have different elements to be
    /// in different languages, like resx files, comments, identifiers etc.
    /// Has internal cache to not load dictionaries from disc on
    /// each request.
    /// </summary>
    public class SpellCheckManager
    {
        private static readonly Dictionary<string, SpellChecker> _dictionaryCache =
            new Dictionary<string, SpellChecker>();

        /// <summary>
        /// Retrieves spell checker for resx file spell checking.
        /// </summary>
        /// <param name="resxFile">Instance of resx file.</param>
        /// <param name="defaultResXDictionary">Default dictionary to be used.</param>
        /// <returns><see cref="ISpellChecker"/> instance or null.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ISpellChecker GetSpellChecker(IProjectFile resxFile, string defaultResXDictionary)
        {
            if (!resxFile.Name.ToLower().EndsWith(".resx"))
            {
                throw new ArgumentException("Should be a resx file", "resxFile");
            }

            string[] parts = resxFile.Name.Split('.');
            if (parts.Length > 2)
            {
                string dictName = parts[parts.Length - 2];
                try
                {
                    CultureInfo.GetCultureInfo(dictName);
                }
                catch (ArgumentException)
                {
                    return null;
                }
                if (_dictionaryCache.ContainsKey(dictName))
                {
                    return _dictionaryCache[dictName];
                }
                return loadSpellChecker(dictName, resxFile.GetSolution());
            }

            return GetSpellChecker(resxFile.GetSolution(), defaultResXDictionary);
        }

        /// <summary>
        /// Retrieves spell checker for specified culture.
        /// </summary>
        /// <param name="solution">Solution instance.</param>
        /// <param name="dictionaryName">Culture name. Like en-Us. </param>
        /// <returns><see cref="ISpellChecker"/> instance or null.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ISpellChecker GetSpellChecker(ISolution solution, string dictionaryName)
        {
            if (dictionaryName == null)
            {
                return null;
            }

            if (!_dictionaryCache.ContainsKey(dictionaryName))
            {
                SpellChecker spellChecker = loadSpellChecker(dictionaryName, solution);
                if (spellChecker != null)
                {
                    _dictionaryCache.Add(dictionaryName, spellChecker);
                    return spellChecker;
                }

                return null;
            }

            return _dictionaryCache[dictionaryName];
        }

        /// <summary>
        /// Retrieves spell checker to spell check strings that can contain words in many
        /// languages.
        /// </summary>
        /// <param name="solution">Solution instance.</param>
        /// <param name="dictionaryNames">Names of cultures a string can contain words in.</param>
        /// <returns><see cref="ISpellChecker"/> instance or null.</returns>
        public static ISpellChecker GetSpellChecker(ISolution solution, string[] dictionaryNames)
        {
            if (dictionaryNames == null || dictionaryNames.Length == 0)
            {
                return null;
            }

            if (dictionaryNames.Length == 1)
            {
                return GetSpellChecker(solution, dictionaryNames[0]);
            }

            List<ISpellChecker> checkers = new List<ISpellChecker>();
            foreach (string dictionaryName in dictionaryNames)
            {
                ISpellChecker checker = GetSpellChecker(solution, dictionaryName);
                if (checker != null)
                {
                    checkers.Add(checker);
                }
            }
            return new MultilingualSpellchecker(checkers.ToArray());
        }

        /// <summary>
        /// <see cref="SpellCheckManager"/> caches spell checkers for different languages.
        /// This method clears caches.
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Reset()
        {
            _dictionaryCache.Clear();
        }

        private static SpellChecker loadSpellChecker(string name, ISolution solution)
        {
            CodeStyleSettings settings = CodeStyleSettings.GetInstance(solution);
            if (settings == null)
            {
                return null;
            }

            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                 String.Format("Agent Smith\\dic\\{0}.dic", name));
            if (!File.Exists(path))
            {
                path = getDictPath(name);
                if (!File.Exists(path))
                {
                     return null;
                }
            }            
            
            try
            {
                using (TextReader reader = File.OpenText(path))
                {
                    WordDictionary dictionary = new WordDictionary(reader);
                    CustomDictionary customDictionary =
                        settings.CustomDictionaries.GetOrCreateCustomDictionary(name);

                    return new SpellChecker(dictionary, customDictionary);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Failed to load dictionary from path {0},{1}", path, ex);
                return null;
            }
        }

        private static string getDictPath(string dictionaryName)
        {
            return Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath),
                                String.Format("dic\\{0}.dic", dictionaryName));
        }
    }
}