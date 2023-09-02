using static LetsBuildAnAST.Regex_Parser.CaptureTypes;

namespace LetsBuildAnAST.Regex_Parser
{
    public struct CapturedQuantity
    {
        public readonly CaptureType Type { get; }
        public readonly int Value { get; }
        public readonly int Faces { get; }
        public readonly char? Sign { get; }

        public CapturedQuantity(CaptureType type, int value, int faces = 0, char? sign = null)
        {
            Type = type;
            Value = value;
            Faces = faces;
            Sign = sign;
        }

        public override string ToString()
        {
            string die = Type == CaptureType.DIEROLL ? "d" : "";
            return $"type:{Enum.GetName(Type)} value:{Sign}{Value}{die}{Faces}";
        }
    }
}
