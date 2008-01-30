using System;
using AgentSmith.SpellCheck;
using JetBrains.ReSharper.Psi.Parsing;
using NUnit.Framework;

namespace AgentSmith.SpellCheck
{
    [TestFixture]
    public class WordLexerTest
    {        
        [Test]
        public void Test()
        {
            ILexer lexer = new WordLexer(" hello 256th (seconds");
            lexer.Start();
            Assert.AreEqual("hello", lexer.TokenText);
            lexer.Advance();
            Assert.AreEqual("256th", lexer.TokenText);
            lexer.Advance();
            Assert.AreEqual("seconds", lexer.TokenText);
            lexer.Advance();
            Assert.IsNull(lexer.TokenType);
        }
    }    
}
