namespace LetsBuildAnAST.AST_Parser
{
    public struct Token
    {
        /// <summary>
        /// The given token's type. <see cref="TokenType"/>
        /// </summary>
        public TokenType Type { get; }
        /// <summary>
        /// The string value of the given Type.
        /// </summary>
        public string Value { get; }

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}
