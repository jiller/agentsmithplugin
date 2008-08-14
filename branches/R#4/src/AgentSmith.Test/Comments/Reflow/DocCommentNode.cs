using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.Util;

namespace AgentSmith.Test.Comments.Reflow
{
    class DocCommentNode : LeafElement
    {        
        public DocCommentNode(NodeType type, IBuffer buffer, int startOffset, int endOffset):
            base(type, buffer, startOffset, endOffset)
        {
        }

        #region ICSharpCommentNode Members

        public CommentType CommentType
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region ICommentNode Members

        public string CommentText
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region ITokenNode Members

        public JetBrains.ReSharper.Psi.Tree.ITokenNode GetNextToken()
        {
            throw new NotImplementedException();
        }

        public JetBrains.ReSharper.Psi.Tree.ITokenNode GetPrevToken()
        {
            throw new NotImplementedException();
        }

        public JetBrains.ReSharper.Psi.Parsing.TokenNodeType GetTokenType()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ITreeNode Members

        public JetBrains.ReSharper.Psi.Tree.ITreeNode FindNextNode(JetBrains.ReSharper.Psi.Tree.TreeNodePredicate predicate)
        {
            throw new NotImplementedException();
        }

        public JetBrains.ReSharper.Psi.Tree.ITreeNode FindPrevNode(JetBrains.ReSharper.Psi.Tree.TreeNodePredicate predicate)
        {
            throw new NotImplementedException();
        }

        public JetBrains.ReSharper.Psi.Tree.ITreeNode FirstChild
        {
            get { throw new NotImplementedException(); }
        }

        public JetBrains.ReSharper.Psi.Tree.ITreeNode GetNextMeaningfulChild(JetBrains.ReSharper.Psi.Tree.ITreeNode child)
        {
            throw new NotImplementedException();
        }

        public JetBrains.ReSharper.Psi.Tree.ITreeNode LastChild
        {
            get { throw new NotImplementedException(); }
        }

        public JetBrains.ReSharper.Psi.Tree.ITreeNode NextSibling
        {
            get { throw new NotImplementedException(); }
        }

        public JetBrains.ReSharper.Psi.Tree.ITreeNode Parent
        {
            get { throw new NotImplementedException(); }
        }

        public JetBrains.ReSharper.Psi.Tree.ITreeNode PrevSibling
        {
            get { throw new NotImplementedException(); }
        }

        public void SetResolveContextForDummyContainer(JetBrains.ReSharper.Psi.Tree.ITreeNode context)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IElement Members

        public bool Contains(JetBrains.ReSharper.Psi.Tree.IElement other)
        {
            throw new NotImplementedException();
        }

        public JetBrains.ReSharper.Psi.Tree.IElement Copy(JetBrains.ReSharper.Psi.Tree.IElement context)
        {
            throw new NotImplementedException();
        }

        public JetBrains.ReSharper.Psi.Tree.IElement CopyWithResolve()
        {
            throw new NotImplementedException();
        }

        public JetBrains.ReSharper.Psi.Tree.IElement FindElementAt(JetBrains.Util.TextRange treeTextRange)
        {
            throw new NotImplementedException();
        }

        public ICollection<JetBrains.ReSharper.Psi.Tree.IElement> FindElementsAt(int treeTextOffset)
        {
            throw new NotImplementedException();
        }

        public JetBrains.ReSharper.Psi.Resolve.IReference[] FindReferencesAt(JetBrains.Util.TextRange treeTextRange)
        {
            throw new NotImplementedException();
        }

        public JetBrains.ReSharper.Psi.Tree.IElement FindTokenAt(int treeTextOffset)
        {
            throw new NotImplementedException();
        }

        public T GetContainingElement<T>(bool returnThis) where T : JetBrains.ReSharper.Psi.Tree.IElement
        {
            throw new NotImplementedException();
        }

        public JetBrains.ReSharper.Psi.Tree.IElement GetContainingElement(Type t, bool returnThis)
        {
            throw new NotImplementedException();
        }

        public JetBrains.ReSharper.Psi.Tree.IFile GetContainingFile()
        {
            throw new NotImplementedException();
        }

        public JetBrains.DocumentModel.DocumentRange GetDocumentRange()
        {
            throw new NotImplementedException();
        }

        public JetBrains.ReSharper.Psi.PsiManager GetManager()
        {
            throw new NotImplementedException();
        }

        public JetBrains.DocumentModel.DocumentRange GetNavigationRange()
        {
            throw new NotImplementedException();
        }

        public T GetPersistentData<T>(JetBrains.Util.Key<T> key) where T : class
        {
            throw new NotImplementedException();
        }

        public JetBrains.ProjectModel.IProject GetProject()
        {
            throw new NotImplementedException();
        }

        public JetBrains.ProjectModel.IProjectFile GetProjectFile()
        {
            throw new NotImplementedException();
        }

        public JetBrains.ReSharper.Psi.Resolve.IReference[] GetReferences()
        {
            throw new NotImplementedException();
        }

        public string GetText()
        {
            throw new NotImplementedException();
        }

        public StringBuilder GetText(StringBuilder to)
        {
            throw new NotImplementedException();
        }

        public JetBrains.Util.CharArrayRange GetTextAsCharArrayRange()
        {
            throw new NotImplementedException();
        }

        public int GetTextLength()
        {
            throw new NotImplementedException();
        }

        public int GetTreeStartOffset()
        {
            throw new NotImplementedException();
        }

        public JetBrains.Util.TextRange GetTreeTextRange()
        {
            throw new NotImplementedException();
        }

        public bool IsPhysical()
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        public JetBrains.ReSharper.Psi.PsiLanguageType Language
        {
            get { throw new NotImplementedException(); }
        }

        public JetBrains.ReSharper.Psi.LanguageService LanguageService
        {
            get { throw new NotImplementedException(); }
        }

        public void ProcessDescendants(JetBrains.ReSharper.Psi.IRecursiveElementProcessor processor)
        {
            throw new NotImplementedException();
        }

        public void ProcessDescendantsForResolve(JetBrains.ReSharper.Psi.IRecursiveElementProcessor processor)
        {
            throw new NotImplementedException();
        }

        public void PutPersistentData<T>(JetBrains.Util.Key<T> key, T val) where T : class
        {
            throw new NotImplementedException();
        }

        public JetBrains.ReSharper.Psi.Tree.ITreeNode ToTreeNode()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
