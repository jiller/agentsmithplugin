using System;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentSmith.NamingConventions
{
    [QuickFix]
    public class NamingConventionsQuickFix : IQuickFix
    {
        private readonly string[] _newNames;

        private readonly IDeclaration _declaration;

        public NamingConventionsQuickFix(NamingConventionsSuggestion suggestion)
        {
            _declaration = (IDeclaration) suggestion.Element;
            _newNames = suggestion.NewNames;            
        }

        public bool IsAvailable(IUserDataHolder cache)
        {
            return _newNames.Length > 0;            
        }

        public IBulbItem[] Items
        {
            get
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
}