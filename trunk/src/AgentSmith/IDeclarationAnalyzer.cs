using System;
using JetBrains.ReSharper.Daemon.CSharp.Stages;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith
{
    public interface IDeclarationAnalyzer: IDisposable
    {        
        CSharpHighlightingBase[] Analyze(IDeclaration declaration);
    }
}
