using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Caches;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith.MemberMatch
{
    public class Match
    {
        private static readonly Dictionary<AccessRights, int> _accessRightsOrder =
            new Dictionary<AccessRights, int>();

        private static readonly Dictionary<DeclaredElementType, Declaration> _declMap =
            new Dictionary<DeclaredElementType, Declaration>();

        private AccessLevels _accessLevel = AccessLevels.Any;
        private Declaration _declaration = Declaration.Any;
        private string _inheritedFrom;
        private string _markedWithAttribute;
        private FuzzyBool _readonly = FuzzyBool.Maybe;
        private FuzzyBool _static = FuzzyBool.Maybe;

        private ITypeElement _markedWithAttributeType;
        private ITypeElement _inheritedFromType;

        static Match()
        {
            _declMap.Add(CLRDeclaredElementType.CLASS, Declaration.Class);
            _declMap.Add(CLRDeclaredElementType.CONSTANT, Declaration.Constant);
            _declMap.Add(CLRDeclaredElementType.DELEGATE, Declaration.Delegate);
            _declMap.Add(CLRDeclaredElementType.ENUM, Declaration.Enum);
            _declMap.Add(CLRDeclaredElementType.ENUM_MEMBER, Declaration.EnumerationMember);
            _declMap.Add(CLRDeclaredElementType.EVENT, Declaration.Event);
            _declMap.Add(CLRDeclaredElementType.FIELD, Declaration.Field);
            _declMap.Add(CLRDeclaredElementType.INTERFACE, Declaration.Interface);
            //_declMap.Add(CSharpDeclaredElementType. LOCAL_CONSTANT, Declaration.Constant);
            _declMap.Add(CLRDeclaredElementType.LOCAL_VARIABLE, Declaration.Variable);
            _declMap.Add(CLRDeclaredElementType.METHOD, Declaration.Method);
            _declMap.Add(CLRDeclaredElementType.NAMESPACE, Declaration.Namespace);
            _declMap.Add(CLRDeclaredElementType.PARAMETER, Declaration.Parameter);
            _declMap.Add(CLRDeclaredElementType.PROPERTY, Declaration.Property);
            _declMap.Add(CLRDeclaredElementType.STRUCT, Declaration.Struct);

            _accessRightsOrder.Add(AccessRights.PRIVATE, 1);
            _accessRightsOrder.Add(AccessRights.PROTECTED, 2);
            _accessRightsOrder.Add(AccessRights.INTERNAL, 2);
            _accessRightsOrder.Add(AccessRights.PROTECTED_AND_INTERNAL, 2);
            _accessRightsOrder.Add(AccessRights.PROTECTED_OR_INTERNAL, 2);
            _accessRightsOrder.Add(AccessRights.PUBLIC, 3);
            _accessRightsOrder.Add(AccessRights.NONE, 3);
        }

        public Match()
        {
        }

        public Match(Declaration declaration)
        {
            _declaration = declaration;
        }

        public Match(Declaration declaration, AccessLevels accessLevel)
        {
            _accessLevel = accessLevel;
            _declaration = declaration;
        }

        public Match(Declaration declaration, AccessLevels accessLevel, string inheritedFrom)
        {
            _accessLevel = accessLevel;
            _declaration = declaration;
            _inheritedFrom = inheritedFrom;
        }

        public Match(Declaration declaration, AccessLevels accessLevel, string inheritedFrom, string markedWithAttribute)
        {
            _accessLevel = accessLevel;
            _declaration = declaration;
            _inheritedFrom = inheritedFrom;
            _markedWithAttribute = markedWithAttribute;
        }

        public AccessLevels AccessLevel
        {
            get { return _accessLevel; }
            set { _accessLevel = value; }
        }

        public Declaration Declaration
        {
            get { return _declaration; }
            set { _declaration = value; }
        }

        public string InheritedFrom
        {
            get { return _inheritedFrom; }
            set { _inheritedFrom = value; }
        }

        public string MarkedWithAttribute
        {
            get { return _markedWithAttribute; }
            set { _markedWithAttribute = value; }
        }

        public FuzzyBool IsReadonly
        {
            get { return _readonly; }
            set { _readonly = value; }
        }

        public FuzzyBool IsStatic
        {
            get { return _static; }
            set { _static = value; }
        }

        public void Prepare(ISolution solution, PsiManager manager)
        {
            _markedWithAttributeType = null;
            _inheritedFromType = null;
            DeclarationsCacheScope scope = DeclarationsCacheScope.SolutionScope(solution, true);
            IDeclarationsCache cache = manager.GetDeclarationsCache(scope, true);
            if (!string.IsNullOrEmpty(_markedWithAttribute))
            {
                _markedWithAttributeType = cache[_markedWithAttribute] as ITypeElement;
            }

            if (!string.IsNullOrEmpty(_inheritedFrom))
            {
                _inheritedFromType = cache[_inheritedFrom] as ITypeElement;
            }
        }

        public bool IsMatch(IDeclaration declaration, bool useEffectiveRights)
        {
            if (declaration == null)
            {
                return false;
            }
            if (declaration is INamespaceDeclaration)
            {
                return _declaration == Declaration.Any || _declaration == Declaration.Namespace;
            }
            else
            {
                return isDeclMatch(declaration) &&
                       isRightsMatch(declaration, useEffectiveRights) &&
                       markedWithAttributeMatch(declaration) &&
                       inheritsMatch(declaration) &&
                       isReadonlyMatch(declaration) &&
                       isStaticMatch(declaration);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            DeclarationDescription description = DeclarationDescription.DeclDescriptions[_declaration];
            if (description.HasAccessLevel)
            {
                sb.AppendFormat("{0} ", getAccesslevel());
            }
            if (_static != FuzzyBool.Maybe)
            {
                sb.Append(_static == FuzzyBool.True ? "static " : "not static ");
            }
            if (_readonly != FuzzyBool.Maybe)
            {
                sb.Append(_readonly == FuzzyBool.True ? "readonly " : "not readonly ");
            }
            sb.AppendFormat("{0} ", description.Declaration == Declaration.Any ? "declaration " : description.Name.ToLower());
            if (description.CanInherit && !string.IsNullOrEmpty(_inheritedFrom))
            {
                sb.AppendFormat("inherited from '{0}' ", _inheritedFrom);
            }
            if (description.CanBeMarkedWithAttribute && !string.IsNullOrEmpty(_markedWithAttribute))
            {
                sb.AppendFormat("marked with '{0}' ", _markedWithAttribute);
            }
            return sb.ToString();
        }

        private bool isReadonlyMatch(IDeclaration declaration)
        {            
            if (IsReadonly == FuzzyBool.Maybe)
            {
                return true;
            }
            IClassMemberDeclaration decl = declaration as IClassMemberDeclaration;
            return decl != null && (IsReadonly == FuzzyBool.True && decl.IsReadonly ||
                                    IsReadonly == FuzzyBool.False && !decl.IsReadonly);
        }

        private bool isStaticMatch(IDeclaration declaration)
        {
            if (IsStatic == FuzzyBool.Maybe)
            {
                return true;
            }
            IClassMemberDeclaration decl = declaration as IClassMemberDeclaration;
            return decl != null && (IsStatic == FuzzyBool.True && decl.IsStatic ||
                                    IsStatic == FuzzyBool.False && !decl.IsStatic);
        }

        private bool inheritsMatch(IDeclaration declaration)
        {
            if (string.IsNullOrEmpty(_inheritedFrom))
            {
                return true;
            }

            if (_inheritedFromType == null)
            {
                return false;
            }

            ITypeElement typeElement = declaration.DeclaredElement as ITypeElement;
            if (typeElement == null)
            {
                return false;
            }
            return typeElement.IsDescendantOf(_inheritedFromType);
        }

        private bool markedWithAttributeMatch(IDeclaration declaration)
        {
            if (string.IsNullOrEmpty(_markedWithAttribute))
            {
                return true;
            }

            if (_markedWithAttributeType == null)
            {
                return false;
            }

            IAttributesOwner attributesOwner = declaration.DeclaredElement as IAttributesOwner;
            if (attributesOwner == null)
            {
                return false;
            }

            foreach (IAttributeInstance attribute in attributesOwner.GetAttributeInstances(false))
            {
                if (attribute.AttributeType.GetTypeElement() == _markedWithAttributeType)
                {
                    return true;
                }
            }
            return false;
        }

        private bool isRightsMatch(IDeclaration declaration, bool useEffectiveRights)
        {
            if (_accessLevel == AccessLevels.Any)
            {
                return true;
            }

            if (!(declaration is IModifiersOwner))
            {
                return false;
            }

            AccessRights rights = getRights((IModifiersOwner)declaration, useEffectiveRights);
            return AccessLevelMap.Map.ContainsKey(rights) && ((AccessLevelMap.Map[rights] & _accessLevel) != 0);
        }

        private static AccessRights getRights(IModifiersOwner owner, bool useEffectiveRights)
        {
            AccessRights effectiveRights = owner.GetAccessRights();
            if (!useEffectiveRights || !(owner is IClassMemberDeclaration))
            {
                return effectiveRights;
            }

            owner = ((IClassMemberDeclaration)owner).GetContainingTypeDeclaration();
            while (owner != null)
            {
                AccessRights ownerRights = owner.GetAccessRights();
                if (_accessRightsOrder.ContainsKey(ownerRights) &&
                    _accessRightsOrder.ContainsKey(effectiveRights) &&
                    _accessRightsOrder[ownerRights] < _accessRightsOrder[effectiveRights])
                {
                    effectiveRights = ownerRights;
                }
                owner = ((IClassMemberDeclaration)owner).GetContainingTypeDeclaration();
            }

            return effectiveRights;
        }

        private bool isDeclMatch(IDeclaration declaration)
        {
            if (declaration is IAccessorDeclaration)
            {
                return false;
            }

            if (declaration.DeclaredElement != null)
            {
                DeclaredElementType type = declaration.DeclaredElement.GetElementType();
                return _declaration == Declaration.Any ||
                       _declMap.ContainsKey(type) && _declMap[type] == _declaration;
            }
            else
            {
                return false;
            }
        }

        private string getAccesslevel()
        {
            StringBuilder sb = new StringBuilder();

            if (AccessLevel == AccessLevels.Any)
            {
                sb.Append("Any");
            }
            else
            {
                foreach (AccessLevelDescription desc in AccessLevelDescription.Descriptions.Values)
                {
                    if (desc.AccessLevel != AccessLevels.Any && (AccessLevel & desc.AccessLevel) != 0)
                    {
                        if (sb.Length != 0)
                        {
                            sb.Append(", ");
                        }
                        sb.Append(desc.Name);
                    }
                }
            }
            return sb.ToString();
        }
    }
}