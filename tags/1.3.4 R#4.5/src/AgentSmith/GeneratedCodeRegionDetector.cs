using System;
using JetBrains.ReSharper.Psi.CodeStyle;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith
{
    internal class GeneratedCodeRegionDetector
    {
        private readonly string[] _skipRegions = CodeStyleSettingsManager.Instance.CodeStyleSettings.GeneratedCodeRegions;

        private int _inGeneratedCode;

        public bool InGeneratedCode
        {
            get { return (_inGeneratedCode > 0); }
        }

        private bool isRegionToSkip(string regionName)
        {
            return (Array.IndexOf(_skipRegions, regionName.Trim()) >= 0);
        }

        public void Process(IElement element)
        {
            IStartRegionNode startRegionNode = element.ToTreeNode() as IStartRegionNode;
            if ((startRegionNode != null) && isRegionToSkip(startRegionNode.Name))
            {
                _inGeneratedCode++;
            }
            IEndRegionNode endRegionNode = element.ToTreeNode() as IEndRegionNode;
            if (endRegionNode != null)
            {
                IStartRegionNode matchingStartRegionNode = endRegionNode.StartRegion;
                if ((matchingStartRegionNode != null) && isRegionToSkip(matchingStartRegionNode.Name))
                {
                    _inGeneratedCode--;
                }
            }
        }
    }
}