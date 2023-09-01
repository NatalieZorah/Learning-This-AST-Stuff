namespace LetsBuildAnAST.AST_Parser
{
    public static class ASTreeBuilder
    {
        public static ASTree GenerateTree(this Token[] tokens)
        {
            List<Node> nodes = new List<Node>();
            //for (int i = 1; i < tokens.Length; i++)
            //{
            //    if (tokens[i].Type == TokenType.DIE
            //        && tokens[i - 1].Type == TokenType.NUMBER
            //        && tokens[i + 1].Type == TokenType.NUMBER)
            //    {
            //        nodes.Add(new Node(tokens[i],
            //            new Node(tokens[i - 1]),
            //            new Node(tokens[i + 1])));
            //    }
            //}

            foreach (Token token in tokens)
            {
                if (token.Type == TokenType.DIE)
                {
                    nodes.Add(
                        new Node(token, null,
                        new Node(tokens[Array.IndexOf(tokens, token) - 1], token),
                        new Node(tokens[Array.IndexOf(tokens, token) + 1], token)));
                }
            }
            throw new NotImplementedException();
        }
    }
}
