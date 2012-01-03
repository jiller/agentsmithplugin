using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith.Comments.Reflow
{    
    public class DocCommentBlockNode :  IDocCommentBlockNode
    {
        private readonly DocCommentNode _commentNode;
        
        public DocCommentBlockNode(DocCommentNode commentNode)
        {
            _commentNode = commentNode;
        }
        
        #region IDocCommentBlockNode Members

        public XmlNode GetXML(ITypeMember element)
        {
            throw new NotImplementedException();
        }

        public DocCommentError[] GetErrors()
        {
            throw new NotImplementedException();
        }
       
        #endregion

        #region ITreeNode Members
        

        public ITreeNode FirstChild
        {
            get { return _commentNode; }
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
       
        public JetBrains.ProjectModel.IProjectFile GetProjectFile()
        {
            throw new NotImplementedException();
        }

        public string GetText()
        {
            throw new NotImplementedException();
        }

        public IElement FindElementAt(TreeTextRange treeTextRange)
        {
            throw new NotImplementedException();
        }

        public ICollection<IElement> FindElementsAt(TreeOffset treeTextOffset)
        {
            throw new NotImplementedException();
        }

        public IElement FindTokenAt(TreeOffset treeTextOffset)
        {
            throw new NotImplementedException();
        }

        public IReference[] FindReferencesAt(TreeTextRange treeTextRange)
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

        public TreeOffset GetTreeStartOffset()
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

        public PsiLanguageType Language
        {
            get { throw new NotImplementedException(); }
        }

        public LanguageService LanguageService
        {
            get { throw new NotImplementedException(); }
        }
              
        public void ProcessDescendantsForResolve(IRecursiveElementProcessor processor)
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

        public IPsiModule GetPsiModule()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IReference> GetFirstClassReferences()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IReference> GetSecondClassReferences()
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