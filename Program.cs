using System.Diagnostics;
using static LetsBuildAnAST.Regex_Parser.RegexDiceParser;
using static LetsBuildAnAST.AST_Parser.Parser;

internal class Program
{
    static void Main(string[] args)
    {
        string[] diceStrings = new string[10] {
            "1d20+5adv+4d6kh3-2d8kl1-4 -t Coen -c This is a comment",
            "2d8+9+2d4kh1-3d8kl2 -t Hue-Mann -c Fuck that guy",
            "4d20kh1+6adv+1d100-2d10-4 -c Horseshoes and Hand Grenades",
            "9d6kh3+3d8-2+2d8adv -c Frag out",
            "0d10+0d100+0d8+0d12 -c A TEST OF YOUR REFLEXES",
            "7D12kl4-2d8+6+2d100adv -c MYURDUR",
            "2d8+2d6+12d6+12d4+10 -c Blade Swordmage crit baybeee",
            "11d12-11d4+7+2d6kh3+18d4+7 -c Let's see what happens",
            "10d8+5d6+8d4-6 -c riperoonie -t hurdygurdy",
            "12d12+11d10-8d6-2+1d20adv"
        };

        Console.WriteLine(">>Starting test:");
        Stopwatch regexWatch = Stopwatch.StartNew();
        foreach (string diceString in diceStrings)
        {
            RollDiceByString(diceString);
        }
        regexWatch.Stop();
        Console.WriteLine($">>Total time to parse {diceStrings.Length} rolls with Regex: {regexWatch.ElapsedMilliseconds} ms");

        Stopwatch parserWatch = Stopwatch.StartNew();
        foreach (string diceString in diceStrings)
        {
            ParseString(diceString);
        }
        parserWatch.Stop();
        Console.WriteLine($">>Total time to parse {diceStrings.Length} rolls with Parser: {parserWatch.ElapsedMilliseconds} ms");
    }
}
//foreach (Token token in ParseString(diceString2))
//{
//    Console.WriteLine($"TokenType: {token.Type.GetTokenDescription()} :: {token.Value}");
//}
