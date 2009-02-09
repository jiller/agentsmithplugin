using System;

namespace AgentSmith.SpellCheck
{
    /// <summary>
    /// Contains common methods used during spell checking of different elements.
    /// </summary>
    public static class SpellCheckUtil
    {
        /// <summary>
        /// We don't spell check words that are uppercase or contain digits.
        /// </summary>
        /// <param name="word">Word to check/</param>
        /// <returns>true, if word shall be spell checked.</returns>
        public static bool ShouldSpellCheck(string word)
        {
            return word != word.ToUpper() && !containsDigit(word);
        }

        private static bool containsDigit(string text)
        {
            foreach (char c in text)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }
    }
}