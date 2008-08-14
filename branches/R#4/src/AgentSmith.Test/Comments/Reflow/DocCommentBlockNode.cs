using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith.Test.Comments.Reflow
{
    class DocCommentBlockNode :  IDocCommentBlockNode
    {
        private DocCommentNode _commentNode;
        
        public DocCommentBlockNode(DocCommentNode commentNode)
        {
            _commentNode = commentNode;
        }
        
        #region IDocCommentBlockNode Members

        public DocCommentError[] GetErrors()
        {
            throw new NotImplementedException();
        }

        public System.Xml.XmlNode GetXML(JetBrains.ReSharper.Psi.IDeclaredElement element)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ITreeNode Members

        public ITreeNode FindNextNode(TreeNodePredicate predicate)
        {
            throw new NotImplementedException();
        }

        public ITreeNode FindPrevNode(TreeNodePredicate predicate)
        {
            throw new NotImplementedException();
        }

        public ITreeNode FirstChild
        {
            get { throw new NotImplementedException(); }
        }

        public ITreeNode GetNextMeaningfulChild(ITreeNode child)
        {
            throw new NotImplementedException();
        }

        public ITreeNode LastChild
        {
            get { throw new NotImplementedException(); }
        }

        public ITreeNode NextSibling
        {
            get { return null; }
        }

        public ITreeNode Parent
        {
            get { throw new NotImplementedException(); }
        }

        public ITreeNode PrevSibling
        {
            get { throw new NotImplementedException(); }
        }

        public void SetResolveContextForDummyContainer(ITreeNode context)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IElement Members

        public bool Contains(IElement other)
        {
            throw new NotImplementedException();
        }

        public IElement Copy(IElement context)
        {
            throw new NotImplementedException();
        }

        public IElement CopyWithResolve()
        {
            throw new NotImplementedException();
        }

        public IElement FindElementAt(JetBrains.Util.TextRange treeTextRange)
        {
            throw new NotImplementedException();
        }

        public ICollection<IElement> FindElementsAt(int treeTextOffset)
        {
            throw new NotImplementedException();
        }

        public JetBrains.ReSharper.Psi.Resolve.IReference[] FindReferencesAt(JetBrains.Util.TextRange treeTextRange)
        {
            throw new NotImplementedException();
        }

        public IElement FindTokenAt(int treeTextOffset)
        {
            throw new NotImplementedException();
        }

        public T GetContainingElement<T>(bool returnThis) where T : IElement
        {
            throw new NotImplementedException();
        }

        public IElement GetContainingElement(Type t, bool returnThis)
        {
            throw new NotImplementedException();
        }

        public IFile GetContainingFile()
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

        public ITreeNode ToTreeNode()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IUserDataHolder Members

        public T GetData<T>(JetBrains.Util.Key<T> key) where T : class
        {
            throw new NotImplementedException();
        }

        public void PutData<T>(JetBrains.Util.Key<T> key, T val) where T : class
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
