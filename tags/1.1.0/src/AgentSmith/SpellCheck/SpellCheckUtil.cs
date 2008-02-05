using System;

namespace AgentSmith.SpellCheck
{
    public static class SpellCheckUtil
    {
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