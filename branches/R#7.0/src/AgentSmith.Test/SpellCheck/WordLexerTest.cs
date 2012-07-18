using JetBrains.ReSharper.Psi.Parsing;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AgentSmith.SpellCheck {
	[TestClass]
	public class WordLexerTest {
		public TestContext TestContext { get; set; }

		[TestMethod]
		public void Test() {
			testTokens(" hello 256th (seconds", "hello", "256th", "seconds");
			testTokens(" 'word1''  ''word2''", "word1", "word2");
			testTokens("''''", new string[0]);
		}

		private static void testTokens(string word, params string[] tokens) {
			ILexer lexer = new WordLexer(word);
			lexer.Start();
			int i = 0;
			while (lexer.TokenType != null) {
				Assert.AreEqual(tokens[i], lexer.GetCurrTokenText());
				lexer.Advance();
				i++;
			}
			Assert.AreEqual(i, tokens.Length, "Number of tokens returned is different.");
		}
	}
}
