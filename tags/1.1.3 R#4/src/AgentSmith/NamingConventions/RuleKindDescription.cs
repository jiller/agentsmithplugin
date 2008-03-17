using System;
using System.Collections.Generic;

namespace AgentSmith.NamingConventions
{
    internal class RuleKindDescription
    {
        public readonly string Name;
        public readonly RuleKind Rule;
        public readonly bool HasRegex;

        public RuleKindDescription(string name, RuleKind rule, bool hasRegex)
        {
            Name = name;
            Rule = rule;
            HasRegex = hasRegex;
        }

        public static RuleKindDescription[] GetDescriptions()
        {
            List<RuleKindDescription> descriptions = new List<RuleKindDescription>();
            descriptions.Add(new RuleKindDescription("Camel", RuleKind.Camel, false));
            descriptions.Add(new RuleKindDescription("Pascal", RuleKind.Pascal, false));
            descriptions.Add(new RuleKindDescription("Regular Expression Match", RuleKind.MatchesRegex, true));
            descriptions.Add(new RuleKindDescription("Not Matches Regular Expression", RuleKind.NotMatchesRegex, true));
            descriptions.Add(new RuleKindDescription("Upper Case", RuleKind.UpperCase, false));
            descriptions.Add(new RuleKindDescription("No Rule", RuleKind.None, false));
            return descriptions.ToArray();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}