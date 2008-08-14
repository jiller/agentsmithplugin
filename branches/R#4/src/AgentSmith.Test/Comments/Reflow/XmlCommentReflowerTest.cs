using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AgentSmith.Comments.Reflow;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;

namespace AgentSmith.Test.Comments.Reflow
{
    [TestFixture]
    public class XmlCommentReflowerTest
    {
        [Test]
        public void TestReflow()
        {
            string unreflownBlock = @"
<summary>
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

            string reflownBlock = @"
<summary>
    This block needs to be reflown.
    Here goes some crap. Here goes some <c> some code</c> that should not
    be reflown <code> 
blocks shall go
as they are.
</code> Paragraphs start from  new line.

    For example this is new paragraph.
   <list>
        <ul>Xml elements on start of
            line start new paragraph.
        </ul>
    </list>
</summary>";

            Buffer buffer = new Buffer(unreflownBlock);
            DocCommentNode docCommentNode = new DocCommentNode(null, buffer, 0, unreflownBlock.Length);
            DocCommentBlockNode blockNode = new DocCommentBlockNode(docCommentNode);
            XmlCommentReflower reflower = new XmlCommentReflower();
            string result = reflower.Reflow(blockNode);

            Assert.AreEqual(reflownBlock, result);
        }
    }
}
