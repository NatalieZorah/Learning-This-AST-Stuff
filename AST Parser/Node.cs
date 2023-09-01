namespace LetsBuildAnAST.AST_Parser
{
    public class Node
    {
        public Token? Parent { get; set; }
        public Token Token { get; set; }
        public Node[] Children { get; set; }

        public Node(Token token, Token? parent = null, Node? leftChild = null, Node? rightChild = null)
        {
            Parent = parent;
            Token = token;
            Children = new Node[2] { leftChild, rightChild };
        }
    }
}
