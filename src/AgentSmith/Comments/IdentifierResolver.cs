using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AgentSmith.Options;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Impl;
using JetBrains.ReSharper.Psi.Caches;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;


namespace AgentSmith.Comments
{

    public static class IdentifierResolver
    {               

        private static readonly List<string> NamespacePrefixesToIgnore = new List<string>
                                                                             {
                                                                                 "MS.Internal."
                                                                             };

        private static bool isParameter(IClassMemberDeclaration decl, string word)
        {
            IParametersOwnerDeclaration methodDecl = decl as IParametersOwnerDeclaration;

            if (methodDecl != null)
            {
                foreach (IRegularParameterDeclaration parm in methodDecl.ParameterDeclarations)
                {
                    if (parm.DeclaredName == word)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool isClassMemberDeclaration(IClassMemberDeclaration declaration, string word)
        {
            ICSharpTypeDeclaration containingType = declaration.GetContainingTypeDeclaration();
            if (containingType != null)
            {
                string withDot = "." + word;
                foreach (ICSharpTypeMemberDeclaration decl in containingType.MemberDeclarations)
                {
                    if (decl.DeclaredName == word || decl.DeclaredName.EndsWith(withDot))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool isTypeParameter(IClassMemberDeclaration declaration, string word)
        {
            IMethodDeclaration method = declaration as IMethodDeclaration;
            if (method != null)
            {
                foreach (ITypeParameterOfMethodDeclaration decl in method.TypeParameterDeclarations)
                {
                    if (decl.DeclaredName == word)
                    {
                        return true;
                    }
                }
            }

            ICSharpTypeDeclaration containingType = declaration.GetContainingTypeDeclaration();
            if (containingType != null)
            {
                IClassLikeDeclaration classDecl = containingType as IClassLikeDeclaration;
                if (classDecl != null)
                {
                    foreach (ITypeParameterOfTypeDeclaration decl in classDecl.TypeParameters)
                    {
                        if (decl.DeclaredName == word)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /*private static bool isADeclaredElement(string word, ISolution solution, DeclarationCacheLibraryScope scope = DeclarationCacheLibraryScope.FULL)
        {
            CacheManager cacheManager = solution.GetPsiServices().CacheManager;
            IDeclarationsCache declarationsCache = cacheManager.GetDeclarationsCache(scope, true);
            IDeclaredElement[] declaredElements = declarationsCache.GetElementsByShortName(word);
            return declaredElements != null && declaredElements.Length > 0;
        }
        */

        private static List<TypeAndNamespace> getTypeAndNamespaces(IEnumerable<IClrDeclaredElement> declaredElements, IClassMemberDeclaration declaration, ISolution solution, IdentifierLookupScopes scope = IdentifierLookupScopes.ProjectAndReferencedLibraries)
        {
            ICSharpFile file = declaration.GetContainingFile() as ICSharpFile;

            List<TypeAndNamespace> inFileResults = new List<TypeAndNamespace>();
            List<TypeAndNamespace> importResults = new List<TypeAndNamespace>();
            if (declaredElements == null) return inFileResults;
            foreach (IClrDeclaredElement element in declaredElements)
            {
                // Find out whether the given element is available in the current file.
                TypeAndNamespace typeAndNamespace = GetAccessableTypeElementAndNamespace(declaration, solution, file, element, scope);
                if (typeAndNamespace == null) continue;
                if (typeAndNamespace.RequiredNamespaceImport == null)
                {
                    inFileResults.Add(typeAndNamespace);
                }
                else
                {
                    importResults.Add(typeAndNamespace);
                }
            }

            inFileResults.Sort((x, y) => x.XmlDocId.CompareTo(y.XmlDocId));
            importResults.Sort((x, y) => x.XmlDocId.CompareTo(y.XmlDocId));
            inFileResults.AddRange(importResults);
            return inFileResults;
        }

        private static List<TypeAndNamespace> getDeclaredElements(string word, IClassMemberDeclaration declaration, ISolution solution, IdentifierLookupScopes scope = IdentifierLookupScopes.ProjectAndReferencedLibraries)
        {
            CacheManager cacheManager = solution.GetPsiServices().CacheManager;
            IDeclarationsCache declarationsCache = cacheManager.GetDeclarationsCache(scope.AsLibraryScope(), true);
            IClrDeclaredElement[] declaredElements = declarationsCache.GetElementsByShortName(word);
            return getTypeAndNamespaces(declaredElements, declaration, solution, scope);
        }

        public static string ContractCRef(string input, IClassMemberDeclaration declaration, ISolution solution, IdentifierLookupScopes scope = IdentifierLookupScopes.ProjectAndReferencedLibraries)
        {
            string crefText = input;
            if (crefText.Length > 2 && crefText[1] == ':')
            {
                crefText = crefText.Substring(2);
            }

            ICSharpFile file = declaration.GetContainingFile() as ICSharpFile;

            CacheManager cacheManager = solution.GetPsiServices().CacheManager;
            IDeclarationsCache declarationsCache = cacheManager.GetDeclarationsCache(scope.AsLibraryScope(), true);
            ICollection<IClrDeclaredElement> declaredElements = declarationsCache.GetElementsByQualifiedName(crefText);

            if (declaredElements.Count == 0)
            {
                return crefText;
            }

            List<TypeAndNamespace> typesAndNamespaces = getTypeAndNamespaces(declaredElements, declaration, solution, scope);
            if (typesAndNamespaces.Count > 0)
            {
                return typesAndNamespaces[0].XmlDocId;
            }
            return crefText;
        }

        private class TypeAndNamespace
        {
            public string XmlDocId;

            public IClrDeclaredElement TypeElement;

            public INamespace RequiredNamespaceImport;
        }

        private static AccessRights GetAccessRights(IClrDeclaredElement element)
        {
            ITypeElement parentTypeElement = element.GetContainingType();

            IAccessRightsOwner accessRightsOwner = element as IAccessRightsOwner;
            if (accessRightsOwner == null) return AccessRights.PRIVATE;

            IAccessRightsOwner parentAccessRightsOwner = parentTypeElement as IAccessRightsOwner;
            if (parentAccessRightsOwner == null) return accessRightsOwner.GetAccessRights();

            return parentAccessRightsOwner.GetAccessRights();
        }


        private static TypeAndNamespace GetAccessableTypeElementAndNamespace(IClassMemberDeclaration declaration, ISolution solution, ICSharpFile file, IClrDeclaredElement element, IdentifierLookupScopes scope)
        {

            //IPsiModule module = element.Module;

            IXmlDocIdOwner idOwner = element as IXmlDocIdOwner;
            string docId = idOwner == null ? element.ShortName : idOwner.XMLDocId;

            // Get the defining type.
            ITypeElement typeElement = element as ITypeElement ?? element.GetContainingType();
            if (typeElement == null) return null;
           
            // Create the result
            TypeAndNamespace result = new TypeAndNamespace
                                          {
                                              XmlDocId = docId,
                                              TypeElement = element
                                          };

            // Get the namespace it belongs to.
            INamespace namespaceElement = typeElement.GetContainingNamespace();
            string namespaceName = namespaceElement.QualifiedName;

            // Check if we're ignoring this namespace
            foreach (string namespacePrefix in NamespacePrefixesToIgnore)
            {
                if (namespaceName.StartsWith(namespacePrefix)) return null;
            }


            // Check if it would be possible to access the type
            AccessRights elementAccessRights = GetAccessRights(element);
            if (elementAccessRights == AccessRights.PRIVATE)
            {
                return null;
            }

            // Check if the type is defined in this solution
            IList<IDeclaration> declarations = element.GetDeclarations();
            if (declarations.Count == 0) 
            {
                // Assembly is an import so no internal things allowed
                if (elementAccessRights == AccessRights.INTERNAL) return null;
            } 

            
            // Check if the given namespace is already imported in this file.
            if (UsingUtil.CheckAlreadyImported(file, namespaceElement) || declaration.GetContainingNamespaceDeclaration().QualifiedName.StartsWith(namespaceName))
            {
                string newDocId = docId[1] == ':' ? docId.Substring(2) : docId;
                if (newDocId.StartsWith(namespaceName + ".")) newDocId = newDocId.Substring(namespaceName.Length + 1);
                result.XmlDocId = newDocId;
                return result;
            }

            // If we require it to be in project or using scope then this is not a match
            if (scope == IdentifierLookupScopes.ProjectAndUsings || scope == IdentifierLookupScopes.ProjectOnly)
            {
                return null;
            }

            // No - so add in the namespace.
            result.RequiredNamespaceImport = namespaceElement;

            return result;
        }

        public static bool IsIdentifier(IClassMemberDeclaration declaration, ISolution solution, string word, IdentifierLookupScopes scope = IdentifierLookupScopes.ProjectAndReferencedLibraries)
        {
            return isParameter(declaration, word) ||
                   isTypeParameter(declaration, word) ||
                   isClassMemberDeclaration(declaration, word) ||
                   getDeclaredElements(word, declaration, solution, scope).Count > 0;
        }

        public static bool IsKeyword(IClassMemberDeclaration declaration, ISolution solution, string word)
        {
            return KeywordUtil.IsKeyword(word);
        }

        public static IList<string> GetReplaceFormats(IClassMemberDeclaration declaration, ISolution solution,
                                                      string word, IdentifierLookupScopes scope = IdentifierLookupScopes.ProjectAndReferencedLibraries)
        {
            List<string> replaceFormats = new List<string>();

            if (isParameter(declaration, word))
            {
                replaceFormats.Add("<paramref name=\"{0}\"/>");
            }

            if (isTypeParameter(declaration, word))
            {
                replaceFormats.Add("<typeparamref name=\"{0}\"/>");
            }

            if (isClassMemberDeclaration(declaration, word))
            {
                replaceFormats.Add("<see cref=\"{0}\"/>");
            }

            // Find out whether the given element is available in the current file.
            List<TypeAndNamespace> typesAndNamespaces = getDeclaredElements(word, declaration, solution, scope);
            foreach (TypeAndNamespace typeAndNamespace in typesAndNamespaces)
            {
                // It's already imported
                replaceFormats.Add("<see cref=\"" + typeAndNamespace.XmlDocId + "\"/>");
            }

            if (IsKeyword(declaration, solution, word))
            {
                replaceFormats.Add("<see langword=\"{0}\"/>");
            }

            return replaceFormats;
        }

        public static bool AnalyzeForMetaTagging(string word, IEnumerable<Regex> patternsToIgnore)
        {
            foreach (Regex re in patternsToIgnore)
            {
                if (re.IsMatch(word)) return false;
            }
            return true;
        }


    }
}