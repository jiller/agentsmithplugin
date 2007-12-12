using System;
using AgentSmith.NamingConventions;

namespace AgentSmith.Options
{
    public enum OrdingType
    {
        ByVisibilityFirst,
        ByMemberTypeFirst
    }

    public struct MemberDescription
    {
        public readonly AccessLevels AccessLevel;
        public readonly Declaration Declaration;

        public MemberDescription(AccessLevels accessLevel, Declaration declaration)
        {
            AccessLevel = accessLevel;
            Declaration = declaration;
        }
    }

    [Serializable]
    public class MemberOrderSettings
    {
        private bool _memberOrderValidationEnabled = true;
        private OrdingType _ordingType = OrdingType.ByMemberTypeFirst;

        private AccessLevels[] _visibilityOrder = new AccessLevels[]
            {
                AccessLevels.Public,
                AccessLevels.ProtectedInternal,
                AccessLevels.Protected,
                AccessLevels.Internal,
                AccessLevels.Private,
            };

        private Declaration[] _declarationOrder = new Declaration[]
            {
                Declaration.Enum,
                Declaration.Interface,
                Declaration.Struct,
                Declaration.Class,
                Declaration.Delegate,
                Declaration.Field,
                Declaration.Constructor,
                Declaration.Destructor,
                Declaration.Event,
                Declaration.Property,
                Declaration.Indexer,
                Declaration.Operator,
                Declaration.Method
            };

        public AccessLevels[] VisibilityOrder
        {
            get { return _visibilityOrder; }
            set { _visibilityOrder = value; }
        }

        public Declaration[] DeclarationOrder
        {
            get { return _declarationOrder; }
            set { _declarationOrder = value; }
        }

        public bool MemberOrderValidationEnabled
        {
            get { return _memberOrderValidationEnabled; }
            set { _memberOrderValidationEnabled = value; }
        }

        public OrdingType OrdingType
        {
            get { return _ordingType; }
            set { _ordingType = value; }
        }
        
        public MemberDescription[] GetMemberOrder()
        {
            MemberDescription[] order = new MemberDescription[_visibilityOrder.Length * _declarationOrder.Length];
            if (_ordingType == OrdingType.ByMemberTypeFirst)
            {
                for (int i = 0; i < _declarationOrder.Length; i++)
                {
                    for (int j = 0; j < _visibilityOrder.Length; j++)
                    {
                        MemberDescription descr = new MemberDescription(_visibilityOrder[j], _declarationOrder[i]);
                        order[i * _visibilityOrder.Length + j] = descr;
                    }
                }
            }

            if (_ordingType == OrdingType.ByVisibilityFirst)
            {
                for (int j = 0; j < _visibilityOrder.Length; j++)
                {
                    for (int i = 0; i < _declarationOrder.Length; i++)
                    {
                        order[j * _declarationOrder.Length + i] = new MemberDescription(_visibilityOrder[j], _declarationOrder[i]);
                    }
                }
            }

            return order;
        }
    }
}