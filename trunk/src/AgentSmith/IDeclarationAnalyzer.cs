using System;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith
{
    /// <summary>
    /// Declaration analyzers should implement this.
    /// </summary>
    public interface IDeclarationAnalyzer
    {        
        SuggestionBase[] Analyze(IDeclaration declaration);
    }
}
