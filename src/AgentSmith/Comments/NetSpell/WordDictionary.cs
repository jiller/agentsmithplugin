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
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using AgentSmith.Comments.NetSpell;
using AgentSmith.Comments.NetSpell.Affix;
using AgentSmith.Comments.NetSpell.Phonetic;
using JetBrains.Util;

namespace AgentSmith.Comments.NetSpell
{
    /// <summary>
    /// The <see cref="WordDictionary"/> class contains all the logic for managing the word list.
    /// </summary>	
    public class WordDictionary
    {
        private readonly Dictionary<string, Word> _baseWords = new Dictionary<string, Word>();
        private readonly List<PhoneticRule> _phoneticRules = new List<PhoneticRule>();
        private readonly Dictionary<string, AffixRule> _prefixRules = new Dictionary<string, AffixRule>();
        private readonly List<string> _replaceCharacters = new List<string>();
        private readonly Dictionary<string, AffixRule> _suffixRules = new Dictionary<string, AffixRule>();
        private readonly HashSet<string> _userWords = new HashSet<string>();
        private string _tryCharacters = "";

        /// <summary>
        ///     Initializes the dictionary by loading and parsing the
        ///     dictionary file and the user file.
        /// </summary>
        public WordDictionary(TextReader inputDictionary)
        {
            Regex spaceRegx = new Regex(@"[^\s]+", RegexOptions.Compiled);
            MatchCollection partMatches;

            string currentSection = "";
            AffixRule currentRule = null;

            while (inputDictionary.Peek() >= 0)
            {
                string tempLine = inputDictionary.ReadLine().Trim();
                if (tempLine.Length > 0)
                {
                    // check for section flag
                    switch (tempLine)
                    {
                        case "[Copyright]":
                        case "[Try]":
                        case "[Replace]":
                        case "[Prefix]":
                        case "[Suffix]":
                        case "[Phonetic]":
                        case "[Words]":
                            // set current section that is being parsed
                            currentSection = tempLine;
                            break;
                        default:
                            // parse line and place in correct object
                            switch (currentSection)
                            {
                                case "[Copyright]":
                                    //this.Copyright += tempLine + "\r\n";
                                    break;
                                case "[Try]": // ISpell try chars
                                    TryCharacters += tempLine;
                                    break;
                                case "[Replace]": // ISpell replace chars
                                    ReplaceCharacters.Add(tempLine);
                                    break;
                                case "[Prefix]": // MySpell prefix rules
                                case "[Suffix]": // MySpell suffix rules

                                    // split line by white space
                                    partMatches = spaceRegx.Matches(tempLine);

                                    // if 3 parts, then new rule  
                                    if (partMatches.Count == 3)
                                    {
                                        currentRule = new AffixRule();

                                        // part 1 = affix key
                                        currentRule.Name = partMatches[0].Value;
                                        // part 2 = combine flag
                                        if (partMatches[1].Value == "Y")
                                        {
                                            currentRule.AllowCombine = true;
                                        }
                                        // part 3 = entry count, not used

                                        if (currentSection == "[Prefix]")
                                        {
                                            // add to prefix collection
                                            PrefixRules.Add(currentRule.Name, currentRule);
                                        }
                                        else
                                        {
                                            // add to suffix collection
                                            SuffixRules.Add(currentRule.Name, currentRule);
                                        }
                                    }
                                        //if 4 parts, then entry for current rule
                                    else if (partMatches.Count == 4)
                                    {
                                        // part 1 = affix key
                                        if (currentRule.Name == partMatches[0].Value)
                                        {
                                            AffixEntry entry = new AffixEntry();

                                            // part 2 = strip char
                                            if (partMatches[1].Value != "0")
                                            {
                                                entry.StripCharacters = partMatches[1].Value;
                                            }
                                            // part 3 = add chars
                                            entry.AddCharacters = partMatches[2].Value;
                                            // part 4 = conditions
                                            AffixUtility.EncodeConditions(partMatches[3].Value, entry);

                                            currentRule.AffixEntries.Add(entry);
                                        }
                                    }
                                    break;
                                case "[Phonetic]": // ASpell phonetic rules
                                    // split line by white space
                                    partMatches = spaceRegx.Matches(tempLine);
                                    if (partMatches.Count >= 2)
                                    {
                                        PhoneticRule rule = new PhoneticRule();
                                        PhoneticUtility.EncodeRule(partMatches[0].Value, ref rule);
                                        rule.ReplaceString = partMatches[1].Value;
                                        _phoneticRules.Add(rule);
                                    }
                                    break;
                                case "[Words]": // dictionary word list
                                    // splits word into its parts
                                    string[] parts = tempLine.Split('/');
                                    Word tempWord = new Word();
                                    // part 1 = base word
                                    tempWord.Text = parts[0];
                                    // part 2 = affix keys
                                    if (parts.Length >= 2)
                                    {
                                        tempWord.AffixKeys = parts[1];
                                    }
                                    // part 3 = phonetic code
                                    if (parts.Length >= 3)
                                    {
                                        tempWord.PhoneticCode = parts[2];
                                    }

                                    BaseWords.Add(tempWord.Text, tempWord);
                                    break;
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>
        ///     The collection of base words for the dictionary.
        /// </summary>		
        public Dictionary<string, Word> BaseWords
        {
            get { return _baseWords; }
        }

        /// <summary>
        ///     Collection of phonetic rules for this dictionary.
        /// </summary>		
        public List<PhoneticRule> PhoneticRules
        {
            get { return _phoneticRules; }
        }

        /// <summary>
        ///     Collection of affix prefixes for the base words in this dictionary.
        /// </summary>		
        public Dictionary<string, AffixRule> PrefixRules
        {
            get { return _prefixRules; }
        }

        /// <summary>
        ///     List of characters to use when generating suggestions using the near miss strategy.
        /// </summary>		
        public List<string> ReplaceCharacters
        {
            get { return _replaceCharacters; }
        }

        /// <summary>
        ///     Collection of affix suffixes for the base words in this dictionary.
        /// </summary>		
        public Dictionary<string, AffixRule> SuffixRules
        {
            get { return _suffixRules; }
        }

        /// <summary>
        ///     List of characters to try when generating suggestions using the near miss strategy.
        /// </summary>		
        public string TryCharacters
        {
            get { return _tryCharacters; }
            set { _tryCharacters = value; }
        }

        /// <summary>
        ///     List of user entered words in this dictionary
        /// </summary>		
        public HashSet<string> UserWords
        {
            get { return _userWords; }
        }

        /// <summary>
        ///     Searches all contained word lists for word
        /// </summary>
        /// <param name="word" type="string">
        ///     <para>
        ///         The word to search for
        ///     </para>
        /// </param>
        /// <returns>
        ///     Returns true if word is found
        /// </returns>
        public ContainsResult Contains(string word)
        {
            // clean up possible base word list
            List<string> possibleBaseWords = new List<string>();

            // Step 1 Search UserWords
            if (_userWords.Contains(word))
            {
                return new ContainsResult(true, null);
            }

            // Step 2 Search BaseWords
            if (_baseWords.ContainsKey(word))
            {
                return new ContainsResult(true, null);
            }

            // Step 3 Remove suffix, Search BaseWords

            // save suffixed words for use when removing prefix
            List<string> suffixWords = new List<string>();
            // Add word to suffix word list
            suffixWords.Add(word);

            foreach (AffixRule rule in SuffixRules.Values)
            {
                foreach (AffixEntry entry in rule.AffixEntries)
                {
                    string tempWord = AffixUtility.RemoveSuffix(word, entry);
                    if (tempWord != word)
                    {
                        if (_baseWords.ContainsKey(tempWord))
                        {
                            if (verifyAffixKey(tempWord, rule.Name[0]))
                            {
                                return new ContainsResult(true, null);
                            }
                        }

                        if (rule.AllowCombine)
                        {
                            // saving word to check if it is a word after prefix is removed
                            suffixWords.Add(tempWord);
                        }
                        else
                        {
                            // saving possible base words for use in generating suggestions
                            possibleBaseWords.Add(tempWord);
                        }
                    }
                }
            }
            // saving possible base words for use in generating suggestions
            possibleBaseWords.AddRange(suffixWords);

            // Step 4 Remove Prefix, Search BaseWords
            foreach (AffixRule rule in PrefixRules.Values)
            {
                foreach (AffixEntry entry in rule.AffixEntries)
                {
                    foreach (string suffixWord in suffixWords)
                    {
                        string tempWord = AffixUtility.RemovePrefix(suffixWord, entry);
                        if (tempWord != suffixWord)
                        {
                            if (_baseWords.ContainsKey(tempWord))
                            {
                                if (verifyAffixKey(tempWord, rule.Name[0]))
                                {
                                    return new ContainsResult(true, null);
                                }
                            }

                            possibleBaseWords.Add(tempWord);
                        }
                    }
                }
            }

            return new ContainsResult(false, possibleBaseWords.ToArray());
        }

        /// <summary>
        ///     Expands an affix compressed base word
        /// </summary>
        /// <param name="word" type="NetSpell.SpellChecker.Dictionary.Word">
        ///     <para>
        ///         The word to expand
        ///     </para>
        /// </param>
        /// <returns>
        ///     A <see cref="List<string>"/> of words expanded from base word
        /// </returns>
        public List<string> ExpandWord(Word word)
        {
            List<string> suffixWords = new List<string>();
            List<string> words = new List<string>();

            suffixWords.Add(word.Text);
            string prefixKeys = "";

            // check suffix keys first
            foreach (char key in word.AffixKeys)
            {
                if (_suffixRules.ContainsKey(key.ToString(CultureInfo.CurrentUICulture)))
                {
                    AffixRule rule = _suffixRules[key.ToString(CultureInfo.CurrentUICulture)];
                    string tempWord = AffixUtility.AddSuffix(word.Text, rule);
                    if (tempWord != word.Text)
                    {
                        if (rule.AllowCombine)
                        {
                            suffixWords.Add(tempWord);
                        }
                        else
                        {
                            words.Add(tempWord);
                        }
                    }
                }
                else if (_prefixRules.ContainsKey(key.ToString(CultureInfo.CurrentUICulture)))
                {
                    prefixKeys += key.ToString(CultureInfo.CurrentUICulture);
                }
            }

            // apply prefixes
            foreach (char key in prefixKeys)
            {
                AffixRule rule = _prefixRules[key.ToString(CultureInfo.CurrentUICulture)];
                // apply prefix to all suffix words
                foreach (string suffixWord in suffixWords)
                {
                    string tempWord = AffixUtility.AddPrefix(suffixWord, rule);
                    if (tempWord != suffixWord)
                    {
                        words.Add(tempWord);
                    }
                }
            }

            words.AddRange(suffixWords);

            //TraceWriter.TraceVerbose("Word Expanded: {0}; Child Words: {1}", word.Text, words.Count);
            return words;
        }

        /// <summary>
        ///     Generates a phonetic code of how the word sounds
        /// </summary>
        /// <param name="word" type="string">
        ///     <para>
        ///         The word to generated the sound code from
        ///     </para>
        /// </param>
        /// <returns>
        ///     A code of how the word sounds
        /// </returns>
        public string PhoneticCode(string word)
        {
            string tempWord = word.ToUpper();
            string prevWord;
            StringBuilder code = new StringBuilder();

            while (tempWord.Length > 0)
            {
                // save previous word
                prevWord = tempWord;
                foreach (PhoneticRule rule in _phoneticRules)
                {
                    bool begining = tempWord.Length == word.Length ? true : false;
                    bool ending = rule.ConditionCount == tempWord.Length ? true : false;

                    if ((rule.BeginningOnly == begining || !rule.BeginningOnly)
                        && (rule.EndOnly == ending || !rule.EndOnly)
                        && rule.ConditionCount <= tempWord.Length)
                    {
                        int passCount = 0;
                        // loop through conditions
                        for (int i = 0; i < rule.ConditionCount; i++)
                        {
                            int charCode = tempWord[i];
                            if ((rule.Condition[charCode] & (1 << i)) == (1 << i))
                            {
                                passCount++; // condition passed
                            }
                            else
                            {
                                break; // rule fails if one condition fails
                            }
                        }
                        // if all condtions passed
                        if (passCount == rule.ConditionCount)
                        {
                            if (rule.ReplaceMode)
                            {
                                tempWord = rule.ReplaceString + tempWord.Substring(rule.ConditionCount - rule.ConsumeCount);
                            }
                            else
                            {
                                if (rule.ReplaceString != "_")
                                {
                                    code.Append(rule.ReplaceString);
                                }
                                tempWord = tempWord.Substring(rule.ConditionCount - rule.ConsumeCount);
                            }
                            break;
                        }
                    }
                } // for each

                // if no match consume one char
                if (prevWord.Length == tempWord.Length)
                {
                    tempWord = tempWord.Substring(1);
                }
            } // while

            return code.ToString();
        }

        /// <summary>
        ///     Verifies the base word has the affix key
        /// </summary>
        /// <param name="word" type="string">
        ///     <para>
        ///         Base word to check
        ///     </para>
        /// </param>
        /// <param name="affixKey" type="string">
        ///     <para>
        ///         Affix key to check 
        ///     </para>
        /// </param>
        /// <returns>
        ///     True if word contains affix key
        /// </returns>
        private bool verifyAffixKey(string word, char affixKey)
        {
            // make sure base word has this affix key
            Word baseWord = BaseWords[word];
            List<char> keys = new List<char>(baseWord.AffixKeys.ToCharArray());
            return keys.Contains(affixKey);
        }
    }
}