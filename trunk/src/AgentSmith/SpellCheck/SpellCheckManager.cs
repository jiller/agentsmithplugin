using System;
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
    public class SpellCheckManager
    {
        private static string _dictionaryName;
        private static SpellChecker _spellChecker;

        public static ISpellChecker GetSpellChecker(IProjectFile file)
        {
            if (file.Name.EndsWith(".resx"))
            {
                string[] parts = file.Name.Split('.');
                if (parts.Length > 2)
                {
                    string dictName = parts[parts.Length - 2];
                    try
                    {
                        CultureInfo.GetCultureInfo(dictName);
                    }
                    catch (ArgumentException)
                    {
                        return GetSpellChecker(file.GetSolution());
                    }
                    if (dictionaryExists(dictName))
                    {
                        return GetSpellChecker(file.GetSolution(), dictName);
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            return GetSpellChecker(file.GetSolution());
        }

        public static ISpellChecker GetSpellChecker(ISolution solution)
        {
            CodeStyleSettings settings = CodeStyleSettings.GetInstance(solution);
            if (settings == null)
            {
                return null;
            }
            return GetSpellChecker(solution, settings.CommentsSettings.DictionaryName);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ISpellChecker GetSpellChecker(ISolution solution, string dictionaryName)
        {
            if (dictionaryName == null)
            {
                return null;
            }
            if (_dictionaryName != dictionaryName)
            {
                string path = getDictPath(dictionaryName);
                if (!File.Exists(path))
                {
                    return null;
                }
                try
                {
                    using (TextReader reader = File.OpenText(path))
                    {
                        WordDictionary dictionary = new WordDictionary(reader);
                        _spellChecker = new SpellChecker(dictionary);
                        _dictionaryName = dictionaryName;
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError("Failed to load dictionary from path {0},{1}", path, ex.ToString());
                    return null;
                }
            }

            CodeStyleSettings settings = CodeStyleSettings.GetInstance(solution);
            if (settings == null)
            {
                return null;
            }

            if (settings.CommentsSettings.UserWords != null)
            {
                _spellChecker.SetUserWords(settings.CommentsSettings.UserWords.Split('\n'));
            }

            return _spellChecker;
        }

        private static string getDictPath(string dictionaryName)
        {
            return Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath),
                String.Format("dic\\{0}.dic", dictionaryName));
        }

        private static bool dictionaryExists(string dictName)
        {
            return File.Exists(getDictPath(dictName));
        }
    }
}