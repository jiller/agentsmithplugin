using System;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Intentions;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentSmith.NamingConventions
{
    [QuickFix]
    public class NamingConventionsQuickFix : IQuickFix
    {
        private readonly IDeclaration _declaration;
        private readonly string[] _newNames;

        public NamingConventionsQuickFix(NamingConventionsSuggestion suggestion)
        {                        
            _declaration = (IDeclaration) suggestion.Element;
            _newNames = suggestion.NewNames;
        }

        #region IQuickFix Members

        public bool IsAvailable(IUserDataHolder cache)
        {
            return _declaration.IsValid();
        }

        public IBulbItem[] Items
        {
            get
            {               
                if (_newNames.Length == 0 || _declaration is INamespaceDeclaration)
                {
                    return new IBulbItem[] {new RenameBulbItem(_declaration)};
                }
                else
                {
                    IBulbItem[] bulbItems = new IBulbItem[_newNames.Length];
                    for (int i = 0; i < _newNames.Length; i++)
                    {
                        bulbItems[i] = new NamingConventionsBulbItem(_declaration, _newNames[i]);
                    }
                    return bulbItems;
                }
            }
        }

        #endregion
    }
}