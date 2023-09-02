using static LetsBuildAnAST.Regex_Parser.CaptureTypes;

namespace LetsBuildAnAST.Regex_Parser
{
    public struct CapturedComment
    {
        public readonly CaptureType Type { get; }
        public readonly string Comment { get; }

        public CapturedComment(CaptureType type, string comment)
        {
            Type = type;
            Comment = comment;
        }

        public override string ToString()
        {
            return $"type:{Type} value:{Comment}";
        }
    }
}
