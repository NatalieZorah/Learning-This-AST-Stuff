using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LetsBuildAnAST.Regex_Parser
{
    public static class RegexDiceParser
    {
        private static Dictionary<string, string> RegexPatterns = new Dictionary<string, string>()
        {
            //{"primary", @"(?<diceRoll>(?<dieCount>\d+)[Dd](?<dieFaces>\d+)){1}(?<modifiers>(?<modifierSign>[+-])((?<modDiceRoll>(?<modDieCount>\d+)[Dd](?<modDieFaces>\d+))|(?<modBonus>\d+)))*((?:\s?)(?<additionalMods>-{1}((?<keepHighest>kh\d+)|(?<keepLowest>kl\d+)|(?<advantage>adv|dis)|(?<target>t\s+\S+)|(?<comment>c\s+(\w+|\s)+))))*"},
            {"primary", @"(?<diceRoll>(?<dieCount>\d+)[Dd](?<dieFaces>\d+){1}\s?((?<keepHighest>kh\d+)|(?<keepLowest>kl\d+)|(?<advantage>adv|dis))*)(?<modifiers>(?<modifierSign>[+-])((?<modDiceRoll>(?<modDieCount>\d+)[Dd](?<modDieFaces>\d+))\s?((?<keepHighest>kh\d+)|(?<keepLowest>kl\d+)|(?<advantage>adv|dis))*|(?<modBonus>\d+)\s?(?<advantage>adv|dis)?))*((?:\s?)(?<additionalMods>-{1}((?<target>t\s+\S+)|(?<comment>c\s+(\w+|\s)+))))*" },
            {"modifierDie", @"(?<modifierSign>[+-])(?<modDiceRoll>(?<modDieCount>\d+)(?:[Dd])(?<modDieFaces>\d+))" },
            {"modifierBonus", @"(?<modifierSign>[+-])(?<modBonus>\d+)"},
            {"keepHighest", @"kh(?<keepHighest>\d+)" },
            {"keepLowest", @"kl(?<keepLowest>\d+)" },
            {"advantage", @"(?<advantage>adv|dis)" },
            {"target", @"t\s+(?<target>\S+)" },
            {"comment", @"c\s+(?<comment>(\w+|\s)+)" }
        };

        public static void RollDiceByString(string diceString)
        {
            string pattern = RegexPatterns["primary"];
            Match match = Regex.Match(diceString, pattern);

            string diceRoll = match.Groups["diceRoll"].Value;
            //Console.WriteLine($"This is the dice roll: {diceRoll}");

            // patterns for modifiers
            string modifierDiePattern = RegexPatterns["modifierDie"];
            string modifierBonusPattern = RegexPatterns["modifierBonus"];

            CaptureCollection capturedModifiers = match.Groups["modifiers"].Captures;

            foreach (Capture capture in capturedModifiers)
            {
                Match modifierDieRoll = Regex.Match(capture.ToString(), modifierDiePattern);
                Match modifierBonus = Regex.Match(capture.ToString(), modifierBonusPattern);

                if (modifierDieRoll.Success)
                {
                    string modSign = modifierDieRoll.Groups["modifierSign"].Value;
                    string dieCount = modifierDieRoll.Groups["modDieCount"].Value;
                    string dieFaces = modifierDieRoll.Groups["modDieFaces"].Value;

                    //Console.WriteLine($"Modifier Dice Roll: {modSign}{dieCount}d{dieFaces}");
                }
                else if (modifierBonus.Success)
                {
                    string modSign = modifierBonus.Groups["modifierSign"].Value;
                    string modBonus = modifierBonus.Groups["modBonus"].Value;

                    //Console.WriteLine($"Modifier Bonus: {modSign}{modBonus}");
                }
            }

            // patterns for additional mods
            string keepHighestPattern = RegexPatterns["keepHighest"];
            string keepLowestPattern = RegexPatterns["keepLowest"];
            string advantagePattern = RegexPatterns["advantage"];
            string targetPattern = RegexPatterns["target"];
            string commentPattern = RegexPatterns["comment"];

            CaptureCollection additionalMods = match.Groups["additionalMods"].Captures;

            foreach (Capture capture in additionalMods)
            {
                Match keepHighestMatch = Regex.Match(capture.ToString(), keepHighestPattern);
                Match keepLowestMatch = Regex.Match(capture.ToString(), keepLowestPattern);
                Match advantageMatch = Regex.Match(capture.ToString(), advantagePattern);
                Match targetMatch = Regex.Match(capture.ToString(), targetPattern);
                Match commentMatch = Regex.Match(capture.ToString(), commentPattern);

                if (keepHighestMatch.Success)
                {
                    string keepHighest = keepHighestMatch.Groups["keepHighest"].Value;
                    //Console.WriteLine($"Keep Highest mod: {keepHighest}");
                }
                else if (keepLowestMatch.Success)
                {
                    string keepLowest = keepLowestMatch.Groups["keepLowest"].Value;
                    //Console.WriteLine($"Keep Lowest mod: {keepLowest}");
                }
                else if (advantageMatch.Success)
                {
                    string advantageString = advantageMatch.Groups["advantage"].Value;
                    string advantage = (advantageString == "adv") ? "Advantage" : "Disadvantage";
                    //Console.WriteLine($"Advantage or Disadvantage: {advantage}");
                }
                else if (targetMatch.Success)
                {
                    string target = targetMatch.Groups["target"].Value;
                    //Console.WriteLine($"Target: {target}");
                }
                else if (commentMatch.Success)
                {
                    string comment = commentMatch.Groups["comment"].Value;
                    //Console.WriteLine($"Comment: {comment}");
                }
            }
        }
    }
}
