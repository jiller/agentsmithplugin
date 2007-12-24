using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AgentSmith.Comments.NetSpell;
using NetSpell.SpellChecker.Dictionary;

namespace NetSpell.SpellChecker
{
    /// <summary>
    ///		The Spelling class encapsulates the functions necessary to check
    ///		the spelling of inputted text.
    /// </summary>	
    public class Spelling
    {
        private readonly WordDictionary _dictionary;

        public Spelling(WordDictionary dictionary)
        {
            _dictionary = dictionary;
        }

        #region ISpell Near Miss Suggetion methods

        /// <summary>
        ///		swap out each char one by one and try all the tryme
        ///		chars in its place to see if that makes a good word
        /// </summary>
        private void BadChar(string word, ICollection<Word> tempSuggestion)
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
                        ws.EditDistance = EditDistance(word, tempWord.ToString());

                        tempSuggestion.Add(ws);
                    }
                }
            }
        }

        /// <summary>
        ///     try omitting one char of word at a time
        /// </summary>
        private void ExtraChar(string word, ICollection<Word> tempSuggestion)
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
                        ws.EditDistance = EditDistance(word, tempWord.ToString());

                        tempSuggestion.Add(ws);
                    }
                }
            }
        }

        /// <summary>
        ///     try inserting a tryme character before every letter
        /// </summary>
        private void ForgotChar(string word, ICollection<Word> tempSuggestion)
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
                        ws.EditDistance = EditDistance(word, tempWord.ToString());

                        tempSuggestion.Add(ws);
                    }
                }
            }
        }

        /// <summary>
        ///     suggestions for a typical fault of spelling, that
        ///		differs with more, than 1 letter from the right form.
        /// </summary>
        private void ReplaceChars(string word, ICollection<Word> tempSuggestion)
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
                        ws.EditDistance = EditDistance(word, tempWord);

                        tempSuggestion.Add(ws);
                    }
                    pos = word.IndexOf(key, pos + 1);
                }
            }
        }

        /// <summary>
        ///     try swapping adjacent chars one by one
        /// </summary>
        private void SwapChar(string word, ICollection<Word> tempSuggestion)
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
                    ws.EditDistance = EditDistance(word, tempWord.ToString());

                    tempSuggestion.Add(ws);
                }
            }
        }

        /// <summary>
        ///     split the string into two pieces after every char
        ///		if both pieces are good words make them a suggestion
        /// </summary>
        private void TwoWords(string word, ICollection<Word> tempSuggestion)
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
                    ws.EditDistance = EditDistance(word, tempWord);

                    tempSuggestion.Add(ws);
                }
            }
        }

        #endregion

        #region public methods

        /// <summary>
        ///     Calculates the minimum number of change, inserts or deletes
        ///     required to change firstWord into secondWord
        /// </summary>
        /// <param name="source" type="string">
        ///     <para>
        ///         The first word to calculate
        ///     </para>
        /// </param>
        /// <param name="target" type="string">
        ///     <para>
        ///         The second word to calculate
        ///     </para>
        /// </param>
        /// <param name="positionPriority" type="bool">
        ///     <para>
        ///         set to true if the first and last char should have priority
        ///     </para>
        /// </param>
        /// <returns>
        ///     The number of edits to make firstWord equal secondWord
        /// </returns>
        public int EditDistance(string source, string target, bool positionPriority)
        {
            // i.e. 2-D array
            Array matrix = Array.CreateInstance(typeof(int), source.Length + 1, target.Length + 1);

            // boundary conditions
            matrix.SetValue(0, 0, 0);

            for (int j = 1; j <= target.Length; j++)
            {
                // boundary conditions
                int val = (int)matrix.GetValue(0, j - 1);
                matrix.SetValue(val + 1, 0, j);
            }

            // outer loop
            for (int i = 1; i <= source.Length; i++)
            {
                // boundary conditions
                int val = (int)matrix.GetValue(i - 1, 0);
                matrix.SetValue(val + 1, i, 0);

                // inner loop
                for (int j = 1; j <= target.Length; j++)
                {
                    int diag = (int)matrix.GetValue(i - 1, j - 1);

                    if (source.Substring(i - 1, 1) != target.Substring(j - 1, 1))
                    {
                        diag++;
                    }

                    int deletion = (int)matrix.GetValue(i - 1, j);
                    int insertion = (int)matrix.GetValue(i, j - 1);
                    int match = Math.Min(deletion + 1, insertion + 1);
                    matrix.SetValue(Math.Min(diag, match), i, j);
                } //for j
            } //for i

            int dist = (int)matrix.GetValue(source.Length, target.Length);

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
        ///     Calculates the minimum number of change, inserts or deletes
        ///     required to change firstWord into secondWord
        /// </summary>
        /// <param name="source" type="string">
        ///     <para>
        ///         The first word to calculate
        ///     </para>
        /// </param>
        /// <param name="target" type="string">
        ///     <para>
        ///         The second word to calculate
        ///     </para>
        /// </param>
        /// <returns>
        ///     The number of edits to make firstWord equal secondWord
        /// </returns>
        /// <remarks>
        ///		This method automatically gives priority to matching the first and last char
        /// </remarks>
        public int EditDistance(string source, string target)
        {
            return EditDistance(source, target, true);
        }

        /// <summary>
        ///    
        /// </summary>
        /// <param name="word" type="string">
        ///     <para>
        ///         The word to generate suggestions on
        ///     </para>
        /// </param>
        /// <remarks>
        ///		
        /// </remarks>       
        /// <seealso cref="TestWord"/>
        public IList<string> Suggest(string word)
        {
            ContainsResult result = _dictionary.Contains(word.ToLower());
            if (!result.Contains)
            {
                return Suggest(word, result.PossibleBaseWords);
            }
            return new List<string>();
        }

        /// <summary>
        ///   
        /// </summary>
        /// <remarks>
        ///		<see cref="TestWord"/> must have been called before calling this method
        /// </remarks>                
        public IList<string> Suggest(string incorrectWord, IList<string> possibleBaseWords)
        {
            if (incorrectWord.Length == 0)
            {
                return new List<string>();
            }

            List<Word> tempSuggestion = new List<Word>();

            if ((_suggestionMode == SuggestionEnum.PhoneticNearMiss
                 || _suggestionMode == SuggestionEnum.Phonetic)
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
                                newWord.EditDistance = EditDistance(incorrectWord, expandedWord);
                                tempSuggestion.Add(newWord);
                            }
                        }
                    }
                }
            }

            if (_suggestionMode == SuggestionEnum.PhoneticNearMiss
                || _suggestionMode == SuggestionEnum.NearMiss)
            {
                // suggestions for a typical fault of spelling, that
                // differs with more, than 1 letter from the right form.
                ReplaceChars(incorrectWord, tempSuggestion);

                // swap out each char one by one and try all the tryme
                // chars in its place to see if that makes a good word
                BadChar(incorrectWord, tempSuggestion);

                // try omitting one char of word at a time
                ExtraChar(incorrectWord, tempSuggestion);

                // try inserting a tryme character before every letter
                ForgotChar(incorrectWord, tempSuggestion);

                // split the string into two pieces after every char
                // if both pieces are good words make them a suggestion
                TwoWords(incorrectWord, tempSuggestion);

                // try swapping adjacent chars one by one
                SwapChar(incorrectWord, tempSuggestion);
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

                if (suggestions.Count >= _maxSuggestions && _maxSuggestions > 0)
                {
                    break;
                }
            }

            return suggestions;
        }

        /// <summary>
        ///     Checks to see if the word is in the dictionary
        /// </summary>
        /// <param name="word" type="string">
        ///     <para>
        ///         The word to check
        ///     </para>
        /// </param>
        /// <returns>
        ///     Returns true if word is found in dictionary
        /// </returns>
        public bool TestWord(string word)
        {
            if (_dictionary.Contains(word).Contains)
            {
                return true;
            }
            else if (_dictionary.Contains(word.ToLower()).Contains)
            {
                return true;
            }
            return false;
        }

        #endregion

        
        private readonly int _maxSuggestions = 10;
        private readonly SuggestionEnum _suggestionMode = SuggestionEnum.PhoneticNearMiss;
    }
}