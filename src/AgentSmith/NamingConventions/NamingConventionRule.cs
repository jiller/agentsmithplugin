using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AgentSmith.MemberMatch;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using Match=AgentSmith.MemberMatch.Match;
using RegexMatch = System.Text.RegularExpressions.Match;

namespace AgentSmith.NamingConventions
{
    [Serializable]
    public class NamingConventionRule
    {
        private string _description;
        private bool _isDisabled = false;
        private Match[] _matches;
        private string[] _mustHavePrefixes = new string[] { };
        private string[] _mustHaveSuffixes = new string[] { };
        private string[] _mustNotHavePrefixes = new string[] { };
        private string[] _mustNotHaveSuffixes = new string[] { };
        private Match[] _notMatches;
        private Regex _regex;
        private RuleKind _rule = RuleKind.None;

        public bool IsDisabled
        {
            get { return _isDisabled; }
            set { _isDisabled = value; }
        }

        public Match[] Matches
        {
            get { return _matches; }
            set { _matches = value; }
        }

        public Match[] NotMatches
        {
            get { return _notMatches; }
            set { _notMatches = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string[] MustHavePrefixes
        {
            get { return _mustHavePrefixes; }
            set { _mustHavePrefixes = value ?? new string[] { }; }
        }

        public string[] MustNotHavePrefixes
        {
            get { return _mustNotHavePrefixes; }
            set { _mustNotHavePrefixes = value ?? new string[] { }; }
        }

        public RuleKind Rule
        {
            get { return _rule; }
            set { _rule = value; }
        }

        public string[] MustHaveSuffixes
        {
            get { return _mustHaveSuffixes; }
            set { _mustHaveSuffixes = value ?? new string[] { }; }
        }

        public string[] MustNotHaveSuffixes
        {
            get { return _mustNotHaveSuffixes; }
            set { _mustNotHaveSuffixes = value ?? new string[] { }; }
        }

        public string Regex
        {
            set { _regex = value == null ? null : new Regex(value, RegexOptions.Compiled); }
            get { return _regex == null ? null : _regex.ToString(); }
        }

        public void Prepare(ISolution solution, PsiManager manager)
        {
            if (Matches != null)
            {
                foreach (Match match in Matches)
                {
                    match.Prepare(solution, manager);
                }
            }
            if (NotMatches != null)
            {
                foreach (Match match in NotMatches)
                {
                    match.Prepare(solution, manager);
                }
            }
        }

        public bool IsMatch(IDeclaration declaration)
        {
            if (declaration is IIndexerDeclaration ||
                declaration.DeclaredName.Contains("."))
            {
                return false;
            }

            return ComplexMatchEvaluator.IsMatch(declaration, Matches, NotMatches, false) != null;
        }

        /// <summary>
        /// Checks whether declaration matches this rule.
        /// </summary>        
        public bool Satisfies(IDeclaration declaration, IList<string> exclusions)
        {
            if (!(declaration is INamespaceDeclaration))
            {
                string name = declaration.DeclaredName;
                return checkName(name, exclusions);
            }
            else
            {
                foreach (string part in declaration.DeclaredName.Split('.'))
                {
                    if (!checkName(part, exclusions))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public string[] GetCorrectedNames(IDeclaration declaration, IList<string> exclusions)
        {
            string name = declaration.DeclaredName;
            if (!(declaration is INamespaceDeclaration))
            {
                return getCorrectedName(name, exclusions);
            }
            else
            {
                string[] parts = name.Split('.');
                for (int i = 0; i < parts.Length; i++)
                {
                    string[] correctedNames = getCorrectedName(parts[i], exclusions);
                    parts[i] = correctedNames.Length == 0 ? parts[i] : correctedNames[0];
                }
                return new string[] { string.Join(".", parts) };
            }
        }

        public override string ToString()
        {
            return Description;
        }

        private bool checkName(string name, ICollection<string> exclusions)
        {
            if (exclusions.Contains(name))
            {
                return true;
            }
            if (!checkObligatoryPrefixes(ref name))
            {
                return false;
            }

            if (!checkForbiddenPrefixes(ref name))
            {
                return false;
            }

            if (!checkObligatorySuffixes(ref name))
            {
                return false;
            }

            if (!checkForbiddenSuffixes(ref name))
            {
                return false;
            }

            if (name.Length == 0)
            {
                return true;
            }

            switch (_rule)
            {
                case RuleKind.Pascal:
                    return char.IsUpper(name[0]);
                case RuleKind.Camel:
                    return char.IsLower(name[0]);
                case RuleKind.UpperCase:
                    return name.ToUpper() == name;
                case RuleKind.NotMatchesRegex:
                    return !_regex.IsMatch(name);
                case RuleKind.MatchesRegex:
                    return _regex.IsMatch(name);
            }

            return true;
        }

        private bool checkForbiddenSuffixes(ref string name)
        {
            if (_mustNotHaveSuffixes == null || _mustNotHaveSuffixes.Length == 0)
            {
                return true;
            }
            if (_mustNotHaveSuffixes.Length > 0)
            {
                foreach (string suffix in _mustNotHaveSuffixes)
                {
                    if (name.ToLower().EndsWith(suffix.ToLower()))
                    {
                        name = name.Substring(0, name.Length - suffix.Length);
                        return false;
                    }
                }
            }
            return true;
        }

        private bool checkObligatorySuffixes(ref string name)
        {
            if (_mustHaveSuffixes == null || _mustHaveSuffixes.Length == 0)
            {
                return true;
            }

            if (_mustHaveSuffixes.Length > 0)
            {
                foreach (string suffix in _mustHaveSuffixes)
                {
                    if (name.EndsWith(suffix))
                    {
                        name = name.Substring(0, name.Length - suffix.Length);
                        return true;
                    }
                    if (name.ToLower().EndsWith(suffix.ToLower()))
                    {
                        name = name.Substring(0, name.Length - suffix.Length);
                        return false;
                    }
                }
            }
            return false;
        }

        private bool checkForbiddenPrefixes(ref string name)
        {
            if (_mustNotHavePrefixes == null || _mustNotHavePrefixes.Length == 0)
            {
                return true;
            }
            if (_mustNotHavePrefixes.Length > 0)
            {
                foreach (string prefix in _mustNotHavePrefixes)
                {
                    if (name.ToLower().StartsWith(prefix.ToLower()))
                    {
                        name = name.Substring(prefix.Length);
                        return false;
                    }
                }
            }

            return true;
        }

        private bool checkObligatoryPrefixes(ref string name)
        {
            if (_mustHavePrefixes == null || _mustHavePrefixes.Length == 0)
            {
                return true;
            }
            if (_mustHavePrefixes.Length > 0)
            {
                foreach (string prefix in _mustHavePrefixes)
                {
                    if (name.StartsWith(prefix))
                    {
                        name = name.Substring(prefix.Length);
                        return true;
                    }
                    if (name.ToLower().StartsWith(prefix.ToLower()))
                    {
                        name = name.Substring(prefix.Length);
                        return false;
                    }
                }
            }
            return false;
        }

        private string[] getCorrectedName(string name, ICollection<string> exclusions)
        {
            if (exclusions.Contains(name))
            {
                return new string[] { name };
            }
            string originalName = name;
            bool forceError = false;
            checkForbiddenPrefixes(ref name);
            checkForbiddenSuffixes(ref name);
            bool checkedObligatoryPrefixes = checkObligatoryPrefixes(ref name);
            bool checkedObligatorySuffixes = checkObligatorySuffixes(ref name);
            if (name.Length > 0)
            {
                switch (_rule)
                {
                    case RuleKind.Camel:
                        name = char.ToLower(name[0]) + name.Substring(1);
                        break;
                    case RuleKind.Pascal:
                        name = char.ToUpper(name[0]) + name.Substring(1);
                        break;
                    case RuleKind.UpperCase:
                        name = name.ToUpper();
                        break;
                    case RuleKind.NotMatchesRegex:
                        foreach (RegexMatch match in _regex.Matches(name))
                        {
                            if (match.Groups["remove"] != null && match.Groups["remove"].Success)
                            {
                                name = name.Replace(match.Groups["remove"].Captures[0].Value, "");
                            }
                            else
                            {
                                forceError = true;
                            }
                        }
                        break;
                    case RuleKind.MatchesRegex:
                        if (!_regex.IsMatch(name))
                        {
                            forceError = true;
                        }
                        break;
                }
            }

            string[] returnNames = new string[] { name };
            if (name != originalName)
            {
                forceError = true;
            }

            if (!checkedObligatoryPrefixes)
            {
                string[] prefixedNames = new string[_mustHavePrefixes.Length];
                for (int i = 0; i < _mustHavePrefixes.Length; i++)
                {
                    prefixedNames[i] = _mustHavePrefixes[i] + returnNames[0];
                }
                returnNames = prefixedNames;
                forceError = true;
            }

            if (!checkedObligatorySuffixes)
            {
                string[] newNames = new string[_mustHaveSuffixes.Length * returnNames.Length];
                for (int i = 0; i < _mustHaveSuffixes.Length; i++)
                {
                    for (int j = 0; j < returnNames.Length; j++)
                    {
                        newNames[i + j * returnNames.Length] = returnNames[j] + _mustHaveSuffixes[i];
                    }
                }
                returnNames = newNames;
                forceError = true;
            }

            if (forceError)
            {
                return returnNames;
            }
            else
            {
                return new string[0];
            }
        }
    }
}