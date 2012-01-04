using System.Collections.Generic;
using System.Text.RegularExpressions;

using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.Caches;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith.Comments
{
    public static class IdentifierResolver
    {               
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

        private static bool isADeclaredElement(string word, ISolution solution, DeclarationCacheLibraryScope scope = DeclarationCacheLibraryScope.FULL)
        {
            CacheManager cacheManager = solution.GetPsiServices().CacheManager;
            IDeclarationsCache declarationsCache = cacheManager.GetDeclarationsCache(scope, true);
            IDeclaredElement[] declaredElements = declarationsCache.GetElementsByShortName(word);
            return declaredElements != null && declaredElements.Length > 0;
        }

        public static bool IsIdentifier(IClassMemberDeclaration declaration, ISolution solution, string word, DeclarationCacheLibraryScope scope = DeclarationCacheLibraryScope.FULL)
        {
            return isParameter(declaration, word) ||
                   isTypeParameter(declaration, word) ||
                   isClassMemberDeclaration(declaration, word) ||
                   isADeclaredElement(word, solution, scope);
        }

        public static bool IsKeyword(IClassMemberDeclaration declaration, ISolution solution, string word)
        {
            return KeywordUtil.IsKeyword(word);
        }

        public static IList<string> GetReplaceFormats(IClassMemberDeclaration declaration, ISolution solution,
                                                      string word)
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

            if (isClassMemberDeclaration(declaration, word) ||
                isADeclaredElement(word, solution))
            {
                replaceFormats.Add("<see cref=\"{0}\"/>");
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