using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AgentSmith.SpellCheck
{
    public class PatternRemover
    {        
        public static string RemovePatterns(string buffer, IList<Regex> patterns)
        {
            foreach (Regex pattern in patterns)
            {
                buffer = pattern.Replace(buffer, Evaluator);
            }
            return buffer;
        }

        private static string Evaluator(Match match)
        {
            return new string(' ', match.Groups[0].Value.Length);            
        }
    }
}
