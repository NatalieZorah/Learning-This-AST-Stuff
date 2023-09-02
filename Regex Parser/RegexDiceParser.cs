using System.Text.RegularExpressions;
using static LetsBuildAnAST.Regex_Parser.CaptureTypes;

namespace LetsBuildAnAST.Regex_Parser
{
    public static class RegexDiceParser
    {
        private static Dictionary<string, string> RegexPatterns = new Dictionary<string, string>()
        {
            {"primary", @"(?<diceRoll>(?<dieCount>\d+)[Dd](?<dieFaces>\d+)\s?(?<rollModifiers>(?<keepHighest>kh\d+)|(?<keepLowest>kl\d+)|(?<advantage>adv|dis))*)\s?(?<modifiers>(?<modifierSign>[-+])((?<modDice>(?<dieCount>\d+)[Dd](?<dieFaces>\d+)\s?(?<rollModifiers>(?<keepHighest>kh\d+)|(?<keepLowest>kl\d+)|(?<advantage>adv|dis))*)|(?<modBonus>\d+)))*(?<comments>\s?-((?<target>t\s\S+)|(?<comment>c\s(\S+|\s?)*)))*" },
            {"diceRoll", @"(?<dieCount>\d+)[Dd](?<dieFaces>\d+)"},
            {"modifierDie", @"(?<modifierSign>[+-])(?<modDiceRoll>(?<modDieCount>\d+)(?:[Dd])(?<modDieFaces>\d+))"},
            {"modifierBonus", @"(?<modifierSign>[+-])(?<modBonus>\d+)"},
            {"keepHighest", @"kh(?<keepHighest>\d+)"},
            {"keepLowest", @"kl(?<keepLowest>\d+)"},
            {"advantage", @"(?<advantage>adv|dis)"},
            {"target", @"t\s+(?<target>\S+)"},
            {"comment", @"c\s+(?<comment>(\w+|\s)+)"}
        };

        public static void RollDiceByString(string diceString, bool printToConsole)
        {
            string pattern = RegexPatterns["primary"];
            Match match = Regex.Match(diceString, pattern);

            string diceRoll = match.Groups["diceRoll"].Value;
            CaptureCollection capturedModifiers = match.Groups["modifiers"].Captures;
            CaptureCollection capturedComments = match.Groups["comments"].Captures;

            GetCapturedRolls(diceRoll.ProcessDiceRoll(), capturedModifiers.ProcessModifiers(), printToConsole);
            PrintComments(capturedComments.ProcessComments(), printToConsole);
        }

        private static CapturedQuantity ProcessDiceRoll(this string diceRoll)
        {
            string diePattern = RegexPatterns["diceRoll"];
            Match capturedDiceRoll = Regex.Match(diceRoll, diePattern);

            if (capturedDiceRoll.Success)
            {
                int diceCount = int.Parse(capturedDiceRoll.Groups["dieCount"].Value);
                int diceFaces = int.Parse(capturedDiceRoll.Groups["dieFaces"].Value);
                return new CapturedQuantity(CaptureType.DIEROLL, diceCount, diceFaces);
            }
            throw new Exception("There was an error in the parsing.");
        }

        private static CapturedQuantity[] ProcessModifiers(this CaptureCollection capturedModifiers)
        {
            string modifierDiePattern = RegexPatterns["modifierDie"];
            string modifierBonusPattern = RegexPatterns["modifierBonus"];
            string keepHighestPattern = RegexPatterns["keepHighest"];
            string keepLowestPattern = RegexPatterns["keepLowest"];
            string advantagePattern = RegexPatterns["advantage"];

            List<CapturedQuantity> modifiers = new List<CapturedQuantity>();

            foreach (Capture capture in capturedModifiers)
            {
                Match modifierDieRoll = Regex.Match(capture.ToString(), modifierDiePattern);
                Match modifierBonus = Regex.Match(capture.ToString(), modifierBonusPattern);
                Match keepHighestMatch = Regex.Match(capture.ToString(), keepHighestPattern);
                Match keepLowestMatch = Regex.Match(capture.ToString(), keepLowestPattern);
                Match advantageMatch = Regex.Match(capture.ToString(), advantagePattern);

                if (keepHighestMatch.Success)
                {
                    int keepHighest = int.Parse(keepHighestMatch.Groups["keepHighest"].Value);
                    modifiers.Add(new CapturedQuantity(CaptureType.KEEP_H, keepHighest));
                }
                else if (keepLowestMatch.Success)
                {
                    int keepLowest = int.Parse(keepLowestMatch.Groups["keepLowest"].Value);
                    modifiers.Add(new CapturedQuantity(CaptureType.KEEP_L, keepLowest));
                }
                else if (advantageMatch.Success)
                {
                    string advantageString = advantageMatch.Groups["advantage"].Value;
                    switch (advantageString)
                    {
                        case "adv":
                            modifiers.Add(new CapturedQuantity(CaptureType.ADVANTAGE, 0));
                            break;
                        case "dis":
                            modifiers.Add(new CapturedQuantity(CaptureType.DISADVANTAGE, 0));
                            break;
                    }
                }
                else if (modifierDieRoll.Success)
                {
                    char modSign = char.Parse(modifierDieRoll.Groups["modifierSign"].Value);
                    int dieCount = int.Parse(modifierDieRoll.Groups["modDieCount"].Value);
                    int dieFaces = int.Parse(modifierDieRoll.Groups["modDieFaces"].Value);
                    modifiers.Add(new CapturedQuantity(CaptureType.DIEROLL, dieCount, dieFaces, modSign));
                }
                else if (modifierBonus.Success)
                {
                    char modSign = char.Parse(modifierBonus.Groups["modifierSign"].Value);
                    int modBonus = int.Parse(modifierBonus.Groups["modBonus"].Value);
                    modifiers.Add(new CapturedQuantity(CaptureType.FLATBONUS, modBonus, 0, modSign));
                }
            }

            return modifiers.ToArray();
        }

        private static CapturedComment[] ProcessComments(this CaptureCollection capturedComments)
        {
            string targetPattern = RegexPatterns["target"];
            string commentPattern = RegexPatterns["comment"];

            List<CapturedComment> comments = new List<CapturedComment>();

            foreach (Capture capturedComment in capturedComments)
            {
                Match targetMatch = Regex.Match(capturedComment.ToString(), targetPattern);
                Match commentMatch = Regex.Match(capturedComment.ToString(), commentPattern);

                if (targetMatch.Success)
                {
                    string target = targetMatch.Groups["target"].Value;
                    comments.Add(new CapturedComment(CaptureType.TARGET, target));
                }
                else if (commentMatch.Success)
                {
                    string comment = commentMatch.Groups["comment"].Value;
                    comments.Add(new CapturedComment(CaptureType.COMMENT, comment));
                }
            }

            return comments.ToArray();
        }

        public static CapturedQuantity[] GetCapturedRolls(
            CapturedQuantity dieRoll,
            CapturedQuantity[] capturedModifiers,
            bool printToConsole)
        {
            if (printToConsole)
            {

                Console.WriteLine(dieRoll.ToString());
                foreach (CapturedQuantity capturedQuantity in capturedModifiers)
                {
                    Console.WriteLine(capturedQuantity.ToString());
                }

            }

            CapturedQuantity[] output = new CapturedQuantity[capturedModifiers.Length + 1];
            output[0] = dieRoll;

            for (int i = 0; i < capturedModifiers.Length; i++)
            {
                output[i + 1] = capturedModifiers[i];
            }

            return output;
        }

        public static void PrintComments(CapturedComment[] capturedComments, bool printToConsole)
        {
            if (printToConsole)
            {
                foreach (CapturedComment capturedComment in capturedComments)
                {
                    Console.WriteLine(capturedComment.ToString());
                }
            }
        }
    }
}
