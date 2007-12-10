using System;
using System.Collections.Generic;
using AgentSmith.Highlights;
using AgentSmith.NamingConventions;
using AgentSmith.Options;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith.MemberOrder
{
    internal class ClassStructureAnalyzer : IDeclarationAnalyzer
    {
        private MemberDescription[] _order;
        private bool _enabled;

        public ClassStructureAnalyzer(MemberOrderSettings settings)
        {
            _enabled = settings.MemberOrderValidationEnabled;
            _order = settings.GetMemberOrder();
        }

        public IHighlighting[] Analyze(IDeclaration declaration)
        {
            if (!_enabled || !(declaration is IClassDeclaration))
                return EmptySuggestionResult.EmptyResult;


            List<IHighlighting> highlightings = new List<IHighlighting>();
            IClassMemberDeclaration[] decls =
                getClassesNotInPlace(((IClassDeclaration) declaration).ClassMemberDeclarations);
            foreach (IClassMemberDeclaration decl in decls)
            {
                highlightings.Add(new DeclarationIsInTheWrongPlace(decl, "The Declaration violates standard order."));
            }
            return highlightings.ToArray();
        }


        public void Dispose()
        {
        }

        private IClassMemberDeclaration[] getClassesNotInPlace(IList<IClassMemberDeclaration> declarations)
        {
            int currentMember = 1;

            while (currentMember < declarations.Count)
            {
                IClassMemberDeclaration previous = declarations[currentMember - 1];
                IClassMemberDeclaration current = declarations[currentMember];

                MemberDescription previousDescr =
                    new MemberDescription(AccessLevelMap.Map[previous.GetAccessRights()], getDeclaration(previous));
                MemberDescription currentDescr =
                    new MemberDescription(AccessLevelMap.Map[current.GetAccessRights()], getDeclaration(current));

                if (Array.IndexOf(_order, previousDescr) > Array.IndexOf(_order, currentDescr))
                    return new IClassMemberDeclaration[] {current};

                currentMember++;
            }

            return new IClassMemberDeclaration[] {};
        }

        private Declaration getDeclaration(IClassMemberDeclaration current)
        {
            foreach (KeyValuePair<Type, Declaration> pair in TypeDeclarationMap.Map)
            {
                if (pair.Key.IsAssignableFrom(current.GetType()))
                {
                    return pair.Value;
                }
            }
            return Declaration.Any;
        }
    }
}