using System;
using System.Collections.Generic;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Caches;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.Util;

namespace AgentSmith.Comments
{
    [QuickFix]
    public class XmlCommentSyntaxQuickFix : IQuickFix
    {
        private const uint MAX_SUGGESTION_COUNT = 5;

        private readonly WordIsNotInDictionarySuggestion _suggestion;

        public XmlCommentSyntaxQuickFix(WordIsNotInDictionarySuggestion suggestion)
        {
            _suggestion = suggestion;
        }

        #region IQuickFix Members

        public bool IsAvailable(IUserDataHolder cache)
        {
            return true;
        }

        public IBulbItem[] Items
        {
            get
            {
                List<IBulbItem> items = new List<IBulbItem>();


                DeclarationsCacheScope scope = DeclarationsCacheScope.SolutionScope(_suggestion.Solution, true);
                PsiManager manager = PsiManager.GetInstance(_suggestion.Solution);

                IMethodDeclaration methodDecl = _suggestion.Declaration as IMethodDeclaration;
                if (methodDecl != null)
                {
                    foreach (IRegularParameterDeclaration parm in methodDecl.ParameterDeclarations)
                    {
                        if (parm.DeclaredName == _suggestion.Word)
                        {
                            items.Add(
                                new ReplaceWordWithBulbItem(_suggestion.Range,
                                                            String.Format("<paramref name=\"{0}\"/>", _suggestion.Word)));
                            break;
                        }
                    }
                }


                IMemberOwnerDeclaration containingType = _suggestion.Declaration.GetContainingTypeDeclaration();
                if (containingType != null)
                {
                    string withDot = "." + _suggestion.Word;
                    foreach (ICSharpTypeMemberDeclaration decl in containingType.MemberDeclarations)
                    {
                        if (decl.DeclaredName == _suggestion.Word || decl.DeclaredName.EndsWith(withDot))
                        {
                            items.Add(
                                new ReplaceWordWithBulbItem(_suggestion.Range,
                                                            String.Format("<see cref=\"{0}\"/>", _suggestion.Word)));
                            break;
                        }
                    }

                    IClassLikeDeclaration classDecl = containingType as IClassLikeDeclaration;
                    if (items.Count == 0 && classDecl != null)
                    {
                        foreach (ITypeParameterOfTypeDeclaration decl in classDecl.TypeParameters)
                        {
                            if (decl.DeclaredName == _suggestion.Word)
                            {
                                items.Add(
                                    new ReplaceWordWithBulbItem(_suggestion.Range,
                                                                String.Format("<typeparamref name=\"{0}\"/>",
                                                                              _suggestion.Word)));
                                break;
                            }
                        }
                    }
                }


                IDeclaredElement[] declaredElements =
                    manager.GetDeclarationsCache(scope, true).GetElementsByShortName(_suggestion.Word);
                if (declaredElements != null && declaredElements.Length > 0)
                {
                    items.Add(
                        new ReplaceWordWithBulbItem(_suggestion.Range,
                                                    String.Format("<see cref=\"{0}\"/>", _suggestion.Word)));
                }

                ISpellChecker spellChecker = SpellChecker.GetInstance(_suggestion.Solution);

                if (spellChecker != null)
                {
                    foreach (string newWord in spellChecker.Suggest(_suggestion.Word, MAX_SUGGESTION_COUNT))
                    {
                        items.Add(new ReplaceWordWithBulbItem(_suggestion.Range, newWord));
                    }
                }

                items.Add(new ReplaceWordWithBulbItem(_suggestion.Range, String.Format("<c>{0}</c>", _suggestion.Word)));
                items.Add(new AddToDictionaryBulbItem(_suggestion.Word, _suggestion.Settings, _suggestion.Range));
                return items.ToArray();
            }
        }

        #endregion
    }
}