using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace AgentSmith.AghA
{
    /// <summary>
    /// 
    /// </summary>
    public class Aaa
    {
        /// <summary>
        /// 
        /// </summary>
        protected int a;

        /// <summary>
        /// 
        /// </summary>
        protected int b
        {
            get
            {
                return 0;
            }
        }
    }
    public class SomethingStack: Stack<int>
    {
        
    }
    public interface IAsd
    {
        event EventHandler A;
    }
        
    [TestFixture]
    public class XmiCFactoryTest
    {
        protected int b;
                
        public XmiCFactoryTest(int a)
        {
            this.b = a;
        }

        public int A
        {
            get
            {
                int b = 0;
                return b;
            }
        }
    }
    /// <summary>
    /// A.
    /// </summary>
    public interface IA
    {
        /// <summary>
        /// B.
        /// </summary>                 
        ///<exception cref="ApplicationException"/>
        void Dob();
    }
    
    /// <summary>
    /// A.
    /// </summary>
    public class TestClass: IA
    {

        /// <summary>
        /// A.
        /// </summary>        
        ///<exception cref="ApplicationException"/>
        public void Dob()
        {
            Doa();            
        }
        
        private class Aex:Exception
        {}

        public class Bex : Exception
        { }
        
        
        ///<summary /><exception cref="System.ApplicationException" /><exception cref="AgentSmith.AghA.Aex" />
        ///<exception cref="Bex" />
        
        
        public void Doa()
        {
            try
            {                
                throw new Bex();                
            }
            catch (ApplicationException)
            {
                throw new Aex();
            }
            catch (Aex)
            {
                throw new ApplicationException();
            }
        }
    }
}
