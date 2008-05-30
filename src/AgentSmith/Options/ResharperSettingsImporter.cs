using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using AgentSmith.MemberMatch;
using AgentSmith.NamingConventions;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CodeStyle;
using JetBrains.ReSharper.Psi.Naming.DefaultNamingStyle;
using JetBrains.Shell;

namespace AgentSmith.Options
{
    internal class ResharperSettingsImporter
    {
        public static NamingConventionRule[] GetRules(NamingConventionRule[] existingRules, ISolution solution)
        {
            JetBrains.ReSharper.Psi.CodeStyle.CodeStyleSettings settings = Shell.Instance.IsTestShell ?
                                             CodeStyleSettingsManager.Instance.CodeStyleSettings :
                                             SolutionCodeStyleSettings.GetInstance(solution).CodeStyleSettings;

            DefaultNamingStyleSettings namingSettings = settings.GetNamingSettings();

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
            fillPrefixAndSuffix(staticFieldsRule, namingSettings.FieldNameSettings.Prefix,
                namingSettings.FieldNameSettings.Suffix);

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

            List<NamingConventionRule> conflictingRules = new List<NamingConventionRule>();
            foreach (NamingConventionRule existingRule in existingRules)
            {
                if (existingRule.IsDisabled)
                {
                    continue;
                }

                foreach (NamingConventionRule newRule in new NamingConventionRule[]
                    {
                        fieldsRule,
                        staticFieldsRule,
                        parametersRule,
                        variablesRule
                    })
                {
                    foreach (Match match in existingRule.Matches)
                    {
                        if (match.Declaration == newRule.Matches[0].Declaration &&
                            (existingRule.MustHavePrefixes.Length > 0 && newRule.MustHavePrefixes.Length > 0 ||
                             existingRule.MustHaveSuffixes.Length > 0 && newRule.MustHaveSuffixes.Length > 0))
                        {
                            conflictingRules.Add(existingRule);
                        }
                    }
                }
            }

            if (conflictingRules.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Following rules conflict with configured by you ReSharper settings and were disabled:");
                foreach (NamingConventionRule conflictingRule in conflictingRules)
                {
                    sb.AppendLine(conflictingRule.Description);
                    conflictingRule.IsDisabled = true;
                }

                MessageBox.Show(sb.ToString());
            }
            return null;
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