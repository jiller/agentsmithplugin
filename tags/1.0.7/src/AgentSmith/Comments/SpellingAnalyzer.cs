using System;
using System.Collections.Generic;
using System.IO;
using AgentSmith.Comments.NetSpell;
using JetBrains.ReSharper.Daemon.CSharp.Stages;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentSmith.Comments
{
    public class SpellingAnalyzer : IDeclarationAnalyzer
    {
        private readonly WordDictionary _dictionary;

        public SpellingAnalyzer()
        {
            using (StreamReader sr = new StreamReader(GetType().Assembly.GetManifestResourceStream("AgentSmith.Comments.en-US.dic")))
            {
                _dictionary = new WordDictionary(sr);
                Logger.LogMessage("Loaded {0} words", _dictionary.BaseWords.Count);
            }
        }

        #region IDeclarationAnalyzer Members

        public CSharpHighlightingBase[] Analyze(IDeclaration declaration)
        {
            string name = declaration.DeclaredName;
            List<CSharpHighlightingBase> highlightings = new List<CSharpHighlightingBase>();
            if (string.IsNullOrEmpty(name))
            {
                return highlightings.ToArray();
            }

            /*foreach (string word in split(name))
            {
                ContainsResult result = _dictionary.Contains(word.ToLower());

                if (!result.Contains)
                {
                    SpellSuggestion suggestion  = new SpellSuggestion(declaration, word, result.PossibleBaseWords);
                    highlightings.Add(suggestion);
                }
            }*/

            return highlightings.ToArray();
        }

        public void Dispose()
        {
        }

        #endregion

        private IEnumerable<string> split(string name)
        {
            int i = 0;
            string currentWord = "";
            while (i < name.Length)
            {
                char c = name[i];
                if ((char.IsDigit(c) || c == '_' || c == '@'))
                {
                    if (currentWord.Length > 0)
                    {
                        yield return currentWord;
                        currentWord = "";
                    }
                }
                else if (char.IsUpper(c))
                {
                    if (currentWord.Length > 0 && char.IsLower(currentWord[currentWord.Length - 1]))
                    {
                        yield return currentWord;
                        currentWord = c.ToString();
                    }
                    else if (currentWord.Length > 0 && char.IsUpper(currentWord[currentWord.Length - 1]) &&
                             i < name.Length - 1 && char.IsLower(name[i + 1]))
                    {
                        yield return currentWord;
                        currentWord = c.ToString();
                    }
                    else
                    {
                        currentWord += c;
                    }
                }
                else
                {
                    currentWord += c;
                }
                i++;
            }
            if (string.IsNullOrEmpty(currentWord))
            {
                yield return currentWord;
            }
        }
    }
}