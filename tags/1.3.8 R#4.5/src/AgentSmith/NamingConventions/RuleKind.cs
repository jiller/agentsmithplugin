using System;

namespace AgentSmith.NamingConventions
{
    public enum RuleKind
    {
        Pascal,
        Camel,
        UpperCase,
        None,
        NotMatchesRegex,
        MatchesRegex
    }
}