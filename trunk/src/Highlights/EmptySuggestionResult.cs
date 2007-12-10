using System;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp.Stages;

namespace AgentSmith.Highlights
{
    public static class EmptySuggestionResult
    {
        private static readonly CSharpHighlightingBase[] _emptyResult = new CSharpHighlightingBase[0];

        public static CSharpHighlightingBase[] EmptyResult
        {
            get { return _emptyResult; }
        }
    }
}
