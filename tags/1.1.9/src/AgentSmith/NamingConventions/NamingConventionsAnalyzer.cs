using System;
using System.Collections.Generic;
using AgentSmith.Options;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith.NamingConventions
{
    public class NamingConventionsAnalyzer : IDeclarationAnalyzer
    {
        private readonly string[] _exclusions = new string[0];
        private readonly NamingConventionRule[] _rules = new NamingConventionRule[0];

        public NamingConventionsAnalyzer(NamingConventionSettings settings, ISolution solution)
        {
            if (settings.Exclusions != null)
            {
                _exclusions = settings.Exclusions;
            }
            if (settings.Rules != null)
            {
                _rules = settings.Rules;
            }

            foreach (NamingConventionRule rule in _rules)
            {
                if (!rule.IsDisabled)
                {
                    rule.Prepare(solution);
                }
            }
        }

        #region IDeclarationAnalyzer Members

        public SuggestionBase[] Analyze(IDeclaration declaration)
        {
            if (declaration == null || declaration.DeclaredName.Length == 0)
            {
                return new SuggestionBase[0];
            }

            List<SuggestionBase> highlightings = new List<SuggestionBase>();

            foreach (NamingConventionRule rule in _rules)
            {
                if (!rule.IsDisabled && rule.IsMatch(declaration))
                {
                    if (!rule.Satisfies(declaration, _exclusions))
                    {
                        highlightings.Add(new NamingConventionsSuggestion(declaration, rule, _exclusions));
                        break;
                    }
                }
            }
            return highlightings.ToArray();
        }

        #endregion
    }
}