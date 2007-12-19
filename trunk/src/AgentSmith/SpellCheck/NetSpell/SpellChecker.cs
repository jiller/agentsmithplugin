#region Copyright

/* Copyright (c) 2003, Paul Welter
* All rights reserved.
*
* Redistribution and use in source and binary forms, with or without
* modification, are permitted provided that the following conditions are met:
*     * Redistributions of source code must retain the above copyright
*       notice, this list of conditions and the following disclaimer.
*     * Redistributions in binary form must reproduce the above copyright
*       notice, this list of conditions and the following disclaimer in the
*       documentation and/or other materials provided with the distribution.
*     * Neither the name of the <organization> nor the
*       names of its contributors may be used to endorse or promote products
*       derived from this software without specific prior written permission.
*
* THIS SOFTWARE IS PROVIDED BY <copyright holder> ``AS IS'' AND ANY
* EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
* WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
* DISCLAIMED. IN NO EVENT SHALL <copyright holder> BE LIABLE FOR ANY
* DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
* (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
* LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
* ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
* (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
* SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/

#endregion Copyright

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using AgentSmith.Options;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.ProjectModel;
using JetBrains.Util;

namespace AgentSmith.SpellCheck.NetSpell
{
    /// <summary>
    /// The <see cref="SpellChecker"/> class encapsulates the functions necessary to check
    ///	the spelling of inputted text.
    /// </summary>	
    public class SpellChecker : ISpellChecker
    {
        private readonly WordDictionary _dictionary;

        private SpellChecker(WordDictionary dictionary)
        {
            _dictionary = dictionary;
        }

        private static string _dictionaryName;
        private static SpellChecker _spellChecker;

        public static ISpellChecker GetInstance(ISolution solution)
        {
            CodeStyleSettings settings = CodeStyleSettings.GetInstance(solution);
            if (settings == null)
            {
                return null;
            }
            return GetInstance(solution, settings.CommentsSettings.DictionaryName);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ISpellChecker GetInstance(ISolution solution, string dictionaryName)
        {
            if (dictionaryName == null)
            {
                return null;
            }
            if (_dictionaryName != dictionaryName)
            {
                string path =
                    Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath),
                        String.Format("dic\\{0}.dic", dictionaryName));
                try
                {
                    using (TextReader reader = File.OpenText(path))
                    {
                        WordDictionary dictionary = new WordDictionary(reader);
                        _spellChecker = new SpellChecker(dictionary);
                        _dictionaryName = dictionaryName;
                    }
                }
                catch
                {
                    Logger.LogMessage("Failed to load dictionary from path {0}", path);
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
                _spellChecker.setUserWords(settings.CommentsSettings.UserWords.Split('\n'));
            }

            return _spellChecker;
        }

        private void setUserWords(string[] words)
        {
            _userWords.Clear();
            _userWords.AddAll(words);
        }

        #region ISpell Near Miss Suggetion methods

        /// <summary>
        ///		swap out each char one by one and try all the tryme
        ///		chars in its place to see if that makes a good word
        /// </summary>
        private void badChar(string word, ICollection<Word> tempSuggestion)
        {
            for (int i = 0; i < word.Length; i++)
            {
                StringBuilder tempWord = new StringBuilder(word);
                char[] tryme = _dictionary.TryCharacters.ToCharArray();
                for (int x = 0; x < tryme.Length; x++)
                {
                    tempWord[i] = tryme[x];
                    if (TestWord(tempWord.ToString()))
                    {
                        Word ws = new Word();
                        ws.Text = tempWord.ToString().ToLower();
                        ws.EditDistance = editDistance(word, tempWord.ToString());

                        tempSuggestion.Add(ws);
                    }
                }
            }
        }

        /// <summary>
        ///     try omitting one char of word at a time
        /// </summary>
        private void extraChar(string word, ICollection<Word> tempSuggestion)
        {
            if (word.Length > 1)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    StringBuilder tempWord = new StringBuilder(word);
                    tempWord.Remove(i, 1);

                    if (TestWord(tempWord.ToString()))
                    {
                        Word ws = new Word();
                        ws.Text = tempWord.ToString().ToLower(CultureInfo.CurrentUICulture);
                        ws.EditDistance = editDistance(word, tempWord.ToString());

                        tempSuggestion.Add(ws);
                    }
                }
            }
        }

        /// <summary>
        /// Try inserting a tryme character before every letter.
        /// </summary>
        private void forgotChar(string word, ICollection<Word> tempSuggestion)
        {
            char[] tryme = _dictionary.TryCharacters.ToCharArray();

            for (int i = 0; i <= word.Length; i++)
            {
                for (int x = 0; x < tryme.Length; x++)
                {
                    StringBuilder tempWord = new StringBuilder(word);

                    tempWord.Insert(i, tryme[x]);
                    if (TestWord(tempWord.ToString()))
                    {
                        Word ws = new Word();
                        ws.Text = tempWord.ToString().ToLower();
                        ws.EditDistance = editDistance(word, tempWord.ToString());

                        tempSuggestion.Add(ws);
                    }
                }
            }
        }

        /// <summary>
        /// Suggestions for a typical fault of spelling, that
        /// differs with more, than 1 letter from the right form.
        /// </summary>
        private void replaceChars(string word, ICollection<Word> tempSuggestion)
        {
            List<string> replacementChars = _dictionary.ReplaceCharacters;
            for (int i = 0; i < replacementChars.Count; i++)
            {
                int split = replacementChars[i].IndexOf(' ');
                string key = replacementChars[i].Substring(0, split);
                string replacement = replacementChars[i].Substring(split + 1);

                int pos = word.IndexOf(key);
                while (pos > -1)
                {
                    string tempWord = word.Substring(0, pos);
                    tempWord += replacement;
                    tempWord += word.Substring(pos + key.Length);

                    if (TestWord(tempWord))
                    {
                        Word ws = new Word();
                        ws.Text = tempWord.ToLower();
                        ws.EditDistance = editDistance(word, tempWord);

                        tempSuggestion.Add(ws);
                    }
                    pos = word.IndexOf(key, pos + 1);
                }
            }
        }

        /// <summary>
        ///     try swapping adjacent chars one by one
        /// </summary>
        private void swapChar(string word, ICollection<Word> tempSuggestion)
        {
            for (int i = 0; i < word.Length - 1; i++)
            {
                StringBuilder tempWord = new StringBuilder(word);

                char swap = tempWord[i];
                tempWord[i] = tempWord[i + 1];
                tempWord[i + 1] = swap;

                if (TestWord(tempWord.ToString()))
                {
                    Word ws = new Word();
                    ws.Text = tempWord.ToString().ToLower();
                    ws.EditDistance = editDistance(word, tempWord.ToString());

                    tempSuggestion.Add(ws);
                }
            }
        }

        /// <summary>
        ///     split the string into two pieces after every char
        ///		if both pieces are good words make them a suggestion
        /// </summary>
        private void twoWords(string word, ICollection<Word> tempSuggestion)
        {
            for (int i = 1; i < word.Length - 1; i++)
            {
                string firstWord = word.Substring(0, i);
                string secondWord = word.Substring(i);

                if (TestWord(firstWord) && TestWord(secondWord))
                {
                    string tempWord = firstWord + " " + secondWord;

                    Word ws = new Word();
                    ws.Text = tempWord.ToLower();
                    ws.EditDistance = editDistance(word, tempWord);

                    tempSuggestion.Add(ws);
                }
            }
        }

        #endregion

        #region public methods

        /// <summary>
        /// Checks if the word is in the dictionary.
        /// </summary>
        /// <param name="word" type="string">        
        /// The word to check.        
        /// </param>
        /// <returns>
        /// Returns true if word is found in dictionary.
        /// </returns>
        public bool TestWord(string word)
        {
            return testWord(word).Contains;
        }

        /// <summary>
        /// </summary>
        /// <param name="word" type="string">
        /// The word to generate suggestions for.        
        /// </param>        
        public IList<string> Suggest(string word, uint maxSuggestions)
        {
            ContainsResult result = testWord(word);
            if (!result.Contains)
            {
                return suggest(word, result.PossibleBaseWords, maxSuggestions);
            }
            return new List<string>();
        }

        /// <summary>
        /// Calculates the minimum number of change, inserts or deletes
        /// required to change source into target.
        /// </summary>
        /// <param name="source" type="string">
        /// The first word to calculate.        
        /// </param>
        /// <param name="target" type="string">        
        /// The second word to calculate.        
        /// </param>
        /// <param name="positionPriority" type="bool">        
        ///  Set to true if the first and last char should have priority.        
        /// </param>
        /// <returns>
        ///  The number of edits to make source equal target.
        /// </returns>
        private int editDistance(string source, string target, bool positionPriority)
        {
            // i.e. 2-D array
            Array matrix = Array.CreateInstance(typeof (int), source.Length + 1, target.Length + 1);

            // boundary conditions
            matrix.SetValue(0, 0, 0);

            for (int j = 1; j <= target.Length; j++)
            {
                // boundary conditions
                int val = (int) matrix.GetValue(0, j - 1);
                matrix.SetValue(val + 1, 0, j);
            }

            // outer loop
            for (int i = 1; i <= source.Length; i++)
            {
                // boundary conditions
                int val = (int) matrix.GetValue(i - 1, 0);
                matrix.SetValue(val + 1, i, 0);

                // inner loop
                for (int j = 1; j <= target.Length; j++)
                {
                    int diag = (int) matrix.GetValue(i - 1, j - 1);

                    if (source.Substring(i - 1, 1) != target.Substring(j - 1, 1))
                    {
                        diag++;
                    }

                    int deletion = (int) matrix.GetValue(i - 1, j);
                    int insertion = (int) matrix.GetValue(i, j - 1);
                    int match = Math.Min(deletion + 1, insertion + 1);
                    matrix.SetValue(Math.Min(diag, match), i, j);
                } //for j
            } //for i

            int dist = (int) matrix.GetValue(source.Length, target.Length);

            // extra edit on first and last chars
            if (positionPriority)
            {
                if (source[0] != target[0])
                {
                    dist++;
                }
                if (source[source.Length - 1] != target[target.Length - 1])
                {
                    dist++;
                }
            }
            return dist;
        }

        /// <summary>
        /// Calculates the minimum number of change, inserts or deletes
        /// required to change source into target.
        /// </summary>
        /// <param name="source" type="string">
        /// The first word to calculate.        
        /// </param>
        /// <param name="target" type="string">        
        /// The second word to calculate.        
        /// </param>
        /// <returns>
        /// The number of edits to make source equal target.
        /// </returns>
        /// <remarks>
        ///	This method automatically gives priority to matching the first and last char.
        /// </remarks>
        private int editDistance(string source, string target)
        {
            return editDistance(source, target, true);
        }

        private IList<string> suggest(string incorrectWord, IList<string> possibleBaseWords, uint maxSuggestions)
        {
            if (incorrectWord.Length == 0)
            {
                return new List<string>();
            }

            List<Word> tempSuggestion = new List<Word>();

            if ((_suggestionMode == Suggestion.PhoneticNearMiss
                 || _suggestionMode == Suggestion.Phonetic)
                && _dictionary.PhoneticRules.Count > 0)
            {
                // generate phonetic code for possible root word
                Hashtable codes = new Hashtable();
                foreach (string tempWord in possibleBaseWords)
                {
                    string tempCode = _dictionary.PhoneticCode(tempWord);
                    if (tempCode.Length > 0 && !codes.ContainsKey(tempCode))
                    {
                        codes.Add(tempCode, tempCode);
                    }
                }

                if (codes.Count > 0)
                {
                    // search root words for phonetic codes
                    foreach (Word word in _dictionary.BaseWords.Values)
                    {
                        if (codes.ContainsKey(word.PhoneticCode))
                        {
                            List<string> words = _dictionary.ExpandWord(word);
                            // add expanded words
                            foreach (string expandedWord in words)
                            {
                                Word newWord = new Word();
                                newWord.Text = expandedWord;
                                newWord.EditDistance = editDistance(incorrectWord, expandedWord);
                                tempSuggestion.Add(newWord);
                            }
                        }
                    }
                }
            }

            if (_suggestionMode == Suggestion.PhoneticNearMiss
                || _suggestionMode == Suggestion.NearMiss)
            {
                // suggestions for a typical fault of spelling, that
                // differs with more, than 1 letter from the right form.
                replaceChars(incorrectWord, tempSuggestion);

                // swap out each char one by one and try all the tryme
                // chars in its place to see if that makes a good word
                badChar(incorrectWord, tempSuggestion);

                // try omitting one char of word at a time
                extraChar(incorrectWord, tempSuggestion);

                // try inserting a tryme character before every letter
                forgotChar(incorrectWord, tempSuggestion);

                // split the string into two pieces after every char
                // if both pieces are good words make them a suggestion
                twoWords(incorrectWord, tempSuggestion);

                // try swapping adjacent chars one by one
                swapChar(incorrectWord, tempSuggestion);
            }

            tempSuggestion.Sort();
            IList<string> suggestions = new List<string>();

            for (int i = 0; i < tempSuggestion.Count; i++)
            {
                string word = (tempSuggestion[i]).Text;

                if (!suggestions.Contains(word))
                {
                    suggestions.Add(word);
                }

                if (suggestions.Count >= maxSuggestions)
                {
                    break;
                }
            }

            return suggestions;
        }

        private ContainsResult testWord(string word)
        {
            if (_userWords.Contains(word))
            {
                return new ContainsResult(true, null);
            }

            ContainsResult result = _dictionary.Contains(word);
            if (result.Contains)
            {
                return result;
            }

            return _dictionary.Contains(word.ToLower());
        }

        #endregion

        //private readonly int _maxSuggestions = 5;
        private readonly Suggestion _suggestionMode = Suggestion.PhoneticNearMiss;
        private readonly HashSet<string> _userWords = new HashSet<string>();
    }
}