using System;
using JetBrains.ReSharper.Daemon.CSharp.Stages;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith
{
    /// <summary>
    /// Declaration analyzers should implement this.
    /// </summary>
    public interface IDeclarationAnalyzer
    {        
        CSharpHighlightingBase[] Analyze(IDeclaration declaration);
    }
}
