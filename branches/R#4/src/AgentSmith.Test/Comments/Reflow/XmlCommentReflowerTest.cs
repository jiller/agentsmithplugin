using System;
using JetBrains.Util;
using NUnit.Framework;
using AgentSmith.Comments.Reflow;

namespace AgentSmith.Test.Comments.Reflow
{
    [TestFixture]
    public class XmlCommentReflowerTest
    {
        [Test]
        public void TestReflow()
        {
            string unreflownBlock = @"///<summary>
    This block needs to be reflown.
    Here goes some crap. Here goes some <c> some code</c> that should not
    be reflown. <code> 
blocks shall go
as they are
</code> Paragraphs start from  new line.

    For example this is new paragraph.
   <list>
        <ul>Xml elements on start of
            line start new paragraph.
        </ul>
    </list>
</summary>";

            string reflownBlock = @"<summary>
    This block needs to be reflown. Here goes some 
    crap. Here goes some <c> some code</c> that 
    should not be reflown. <code> 
blocks shall go
as they are
</code> Paragraphs start from  new line.

    For example this is new paragraph.
   <list>
        <ul>Xml elements on start of line start 
        new paragraph.
        </ul>
    </list>
</summary>";

            doTest(unreflownBlock, reflownBlock);
        }

        [Test]
        public void TestSimple()
        {
            string unreflownBlock = @"///<summary>
word
</summary>";

            string reflownBlock = @"<summary>
word
</summary>";

            doTest(unreflownBlock, reflownBlock);
        }

        private void doTest(string unreflownBlock, string reflownBlock)
        {
            StringBuffer buffer = new StringBuffer(unreflownBlock);            
            DocCommentNode docCommentNode = new DocCommentNode(new MyNodeType(), buffer, 0, unreflownBlock.Length);
            DocCommentBlockNode blockNode = new DocCommentBlockNode(docCommentNode);
            XmlCommentReflower reflower = new XmlCommentReflower();
            string result = reflower.Reflow(blockNode, 50);

            Assert.AreEqual(reflownBlock, result);
        }
    }
}
