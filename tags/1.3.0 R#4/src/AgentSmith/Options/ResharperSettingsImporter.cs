using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using AgentSmith.MemberMatch;
using AgentSmith.NamingConventions;
using JetBrains.ReSharper.Psi.Naming.DefaultNamingStyle;

namespace AgentSmith.Options
{
    internal class ResharperSettingsImporter
    {
        public static bool ReSharperSettingsConfigured(DefaultNamingStyleSettings namingSettings)
        {
            return namingSettings != null && (
                                                 isConfigured(namingSettings.FieldNameSettings) ||
                                                 isConfigured(namingSettings.StaticFieldNameSettings) ||
                                                 isConfigured(namingSettings.ParameterNameSettings) ||
                                                 isConfigured(namingSettings.LocalVariableNameSettings));
        }

        private static bool isConfigured(NameSettings settings)
        {
            return settings.Prefix != null && !string.IsNullOrEmpty(settings.Prefix.Trim()) ||
                   settings.Suffix != null && !string.IsNullOrEmpty(settings.Suffix.Trim());
        }

        public static NamingConventionRule[] GetRules(NamingConventionRule[] existingRules, DefaultNamingStyleSettings namingSettings)
        {
            Match staticField = new Match(Declaration.Field);
            staticField.IsStatic = FuzzyBool.True;

            NamingConventionRule fieldsRule = new NamingConventionRule();
            fieldsRule.Matches = new Match[] { new Match(Declaration.Field) };
            fieldsRule.NotMatches = new Match[] { staticField };
            fieldsRule.Description = "Instance fields must have";
            fillPrefixAndSuffix(fieldsRule, namingSettings.FieldNameSettings.Prefix,
                namingSettings.FieldNameSettings.Suffix);

            NamingConventionRule staticFieldsRule = new NamingConventionRule();
            staticFieldsRule.Matches = new Match[] { staticField };
            staticFieldsRule.Description = "Static fields must have";
            fillPrefixAndSuffix(staticFieldsRule, namingSettings.StaticFieldNameSettings.Prefix,
                namingSettings.StaticFieldNameSettings.Suffix);

            NamingConventionRule parametersRule = new NamingConventionRule();
            parametersRule.Matches = new Match[] { new Match(Declaration.Parameter) };
            parametersRule.Description = "Parameters must have";
            fillPrefixAndSuffix(parametersRule, namingSettings.ParameterNameSettings.Prefix,
                namingSettings.ParameterNameSettings.Suffix);

            NamingConventionRule variablesRule = new NamingConventionRule();
            variablesRule.Matches = new Match[] { new Match(Declaration.Variable) };
            variablesRule.Description = "Variables must have";
            fillPrefixAndSuffix(variablesRule, namingSettings.LocalVariableNameSettings.Prefix,
                namingSettings.LocalVariableNameSettings.Suffix);

            NamingConventionRule[] newRules = new NamingConventionRule[]
                    {
                        fieldsRule,
                        staticFieldsRule,
                        parametersRule,
                        variablesRule
                    };

            //Detects conflicts. Designed to work only in this particular case.
            List<NamingConventionRule> conflictingRules = new List<NamingConventionRule>();
            foreach (NamingConventionRule existingRule in existingRules)
            {
                if (existingRule.IsDisabled)
                {
                    continue;
                }

                bool flag = false;
                foreach (NamingConventionRule newRule in newRules)
                {
                    foreach (Match match in existingRule.Matches)
                    {
                        if (match.Declaration == newRule.Matches[0].Declaration &&
                            (existingRule.MustHavePrefixes.Length > 0 && newRule.MustHavePrefixes.Length > 0 ||
                             existingRule.MustHaveSuffixes.Length > 0 && newRule.MustHaveSuffixes.Length > 0 ||
                             newRule.MustHavePrefixes.Length > 0 && (existingRule.Rule == RuleKind.Camel || existingRule.Rule == RuleKind.Pascal || existingRule.Rule == RuleKind.UpperCase)))
                        {
                            conflictingRules.Add(existingRule);
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                    {
                        break;
                    }
                }
            }

            if (conflictingRules.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Following rules conflict with configured by you ReSharper settings and were disabled:");
                sb.AppendLine();
                foreach (NamingConventionRule conflictingRule in conflictingRules)
                {
                    sb.AppendLine(conflictingRule.Description);
                    conflictingRule.IsDisabled = true;
                }

                MessageBox.Show(sb.ToString(), "Agent Smith", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            List<NamingConventionRule>  modifiedRules = new List<NamingConventionRule>(existingRules);
            foreach (NamingConventionRule rule in newRules)
            {
                if (rule.MustHavePrefixes.Length > 0 || rule.MustHaveSuffixes.Length > 0)
                {
                    modifiedRules.Add(rule);
                }
            }
            
            return modifiedRules.ToArray();
        }

        private static void fillPrefixAndSuffix(NamingConventionRule fieldsRule, string prefix, string suffix)
        {
            bool hasPrefix = false;
            if (prefix != null && prefix.Length > 0)
            {
                fieldsRule.MustHavePrefixes = new string[] { prefix.Trim() };
                fieldsRule.Description += String.Format(" '{0}' prefix", prefix.Trim());
                hasPrefix = true;
            }

            if (suffix != null && suffix.Trim().Length > 0)
            {
                fieldsRule.MustHaveSuffixes = new string[] { suffix.Trim() };
                if (hasPrefix)
                {
                    fieldsRule.Description += " and";
                }

                fieldsRule.Description += String.Format(" '{0}' suffix", suffix.Trim());
            }
            fieldsRule.Description += ".";
        }
    }
}