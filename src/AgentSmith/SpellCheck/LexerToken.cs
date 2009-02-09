using System;

namespace AgentSmith.SpellCheck
{
    /// <summary>
    /// This is token which classes like <see cref="CamelHumpLexer"/> produce on output.
    /// </summary>
    public struct LexerToken
    {
        public string Buffer;
        public int End;
        public int Start;

        public LexerToken(string buffer, int start, int end)
        {
            Buffer = buffer;
            Start = start;
            End = end;
        }

        /// <summary>
        /// Value of token.
        /// </summary>
        public string Value
        {
            get { return Buffer.Substring(Start, Length); }
        }

        /// <summary>
        /// Value length.
        /// </summary>
        public int Length
        {
            get { return End - Start; }
        }
    }
}