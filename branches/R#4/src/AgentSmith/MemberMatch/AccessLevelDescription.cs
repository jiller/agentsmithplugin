using System;
using System.Collections.Generic;

namespace AgentSmith.MemberMatch
{
    public class AccessLevelDescription
    {
        public readonly AccessLevels AccessLevel;
        public readonly string Name;
        public readonly string Description;

        public AccessLevelDescription(AccessLevels accessLevel, string name, string description)
        {
            AccessLevel = accessLevel;
            Name = name;
            Description = description;
        }

        public static Dictionary<AccessLevels, AccessLevelDescription> Descriptions
        {
            get
            {
                Dictionary<AccessLevels, AccessLevelDescription> descrs = new Dictionary<AccessLevels, AccessLevelDescription>();
                descrs.Add(AccessLevels.Any, new AccessLevelDescription(AccessLevels.Any, "Any", "Any possible visibility."));
                descrs.Add(AccessLevels.Internal, new AccessLevelDescription(AccessLevels.Internal, "Internal", "Visible within the same assembly."));
                descrs.Add(AccessLevels.Private, new AccessLevelDescription(AccessLevels.Private, "Private", "Visible only from the same class."));
                descrs.Add(AccessLevels.Protected, new AccessLevelDescription(AccessLevels.Protected, "Protected", "Visible to subclasses."));
                descrs.Add(AccessLevels.ProtectedInternal, new AccessLevelDescription(AccessLevels.ProtectedInternal, "Protected Internal", "Visible to subclasses as well as from everywhere within the same assembly."));
                descrs.Add(AccessLevels.ProtectedAndInternal, new AccessLevelDescription(AccessLevels.ProtectedAndInternal, "Protected and Internal", "Visible to subclasses within the same assembly. Ex: protected member of internal class."));
                descrs.Add(AccessLevels.Public, new AccessLevelDescription(AccessLevels.Public, "Public", "Visible from everywhere."));
                return descrs;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}