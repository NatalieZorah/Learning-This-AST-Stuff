namespace LetsBuildAnAST.AST_Parser
{
    public class ASTree
    {
        public Node[] Nodes { get; set; }
        public ASTree(Node[] nodes)
        {
            Nodes = nodes;
        }
    }
}
