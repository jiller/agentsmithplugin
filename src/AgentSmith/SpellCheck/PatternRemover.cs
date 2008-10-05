using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AgentSmith.SpellCheck
{
    /// <summary>
    /// Removes matching patterns from string.
    /// Used to exclude patterns from spell checking.
    /// </summary>
    public class PatternRemover
    {        
        /// <summary>
        /// Removes patterns <param name="patterns"/> from <param name="buffer"/>.
        /// </summary>        
        /// <returns>String with patterns removed.</returns>
        public static string RemovePatterns(string buffer, IList<Regex> patterns)
        {
            foreach (Regex pattern in patterns)
            {
                buffer = pattern.Replace(buffer, evaluator);
            }
            return buffer;
        }

        private static string evaluator(Match match)
        {
            return new string(' ', match.Groups[0].Value.Length);            
        }
    }
}
