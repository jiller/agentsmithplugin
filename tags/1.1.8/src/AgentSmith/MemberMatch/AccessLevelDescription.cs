using System;
using System.Collections.Generic;

namespace AgentSmith.MemberMatch
{
    public class AccessLevelDescription
    {
        public readonly AccessLevels AccessLevel;
        public readonly string Name;

        public AccessLevelDescription(AccessLevels accessLevel, string name)
        {
            AccessLevel = accessLevel;
            Name = name;
        }

        public static Dictionary<AccessLevels, AccessLevelDescription> Descriptions
        {
            get
            {
                Dictionary<AccessLevels, AccessLevelDescription> descrs = new Dictionary<AccessLevels, AccessLevelDescription>();
                descrs.Add(AccessLevels.Any, new AccessLevelDescription(AccessLevels.Any, "Any"));
                descrs.Add(AccessLevels.Internal, new AccessLevelDescription(AccessLevels.Internal, "Internal"));
                descrs.Add(AccessLevels.Private, new AccessLevelDescription(AccessLevels.Private, "Private"));
                descrs.Add(AccessLevels.Protected, new AccessLevelDescription(AccessLevels.Protected, "Protected"));
                descrs.Add(AccessLevels.ProtectedInternal, new AccessLevelDescription(AccessLevels.ProtectedInternal, "Protected Internal"));
                descrs.Add(AccessLevels.Public, new AccessLevelDescription(AccessLevels.Public, "Public"));
                return descrs;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}