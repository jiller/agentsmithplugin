using System;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith.MemberMatch
{
    public static class ComplexMatchEvaluator
    {
        public static Match IsMatch(IDeclaration decl, Match[] matches, Match[] notMatches, bool useEffectiveRights)
        {
            if (matches == null)
            {
                return null;
            }

            foreach (Match match in matches)
            {
                if (match.IsMatch(decl, useEffectiveRights))
                {
                    if (notMatches != null)
                    {
                        foreach (Match notmatch in notMatches)
                        {
                            if (notmatch.IsMatch(decl, useEffectiveRights))
                            {
                                return null;
                            }
                        }
                    }
                    return match;
                }
            }

            return null;
        }
    }
}