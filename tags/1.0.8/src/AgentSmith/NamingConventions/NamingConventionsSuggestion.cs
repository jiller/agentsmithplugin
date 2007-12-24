using System;
using System.Collections.Generic;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith.NamingConventions
{
    [ConfigurableSeverityHighlighting(NamingConventionsSuggestion.NAME)]
    public class NamingConventionsSuggestion : SuggestionBase
    {
        public const string NAME = "DelcarationDoesntConformToNamingConventions";
        private readonly NamingConventionRule _rule;
        private readonly IList<string> _exclusions;

        public NamingConventionsSuggestion(IDeclaration declaration, NamingConventionRule rule, IList<string> exclusions)
            : base(declaration, rule.Description)
        {
            _rule = rule;
            _exclusions = exclusions;
        }

        public string[] NewNames
        {
            get { return _rule.GetCorrectedNames(Element as IDeclaration, _exclusions); }
        }

        public override Severity Severity
        {
            get { return HighlightingSettingsManager.Instance.Settings.GetSeverity(NAME); }
        }            
    }
}