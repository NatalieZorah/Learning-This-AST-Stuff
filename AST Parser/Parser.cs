namespace LetsBuildAnAST.AST_Parser
{
    public static class Parser
    {
        public static Token[] ParseString(string input)
        {
            //List<string> result = new List<string>();
            string[] splitters = new string[]
            {
                "d",
                "adv",
                "dis",
                "kh",
                "kl",
                "-t",
                "-c",
                "+",
                "-",
                " ",
                "eadv"
            };

            return input.SplitWithDelimiters(splitters).TokenizeString();
        }

        private static string[] SplitWithDelimiters(this string input, string[] delimiters)
        {
            List<string> result = new List<string>();
            int startIndex = 0;

            for (int i = 0; i < input.Length; i++)
            {
                foreach (string delimiter in delimiters)
                {
                    if (input.Substring(i).ToLower().StartsWith(delimiter))
                    {
                        if (i > startIndex)
                        {
                            result.Add(input.Substring(startIndex, i - startIndex));
                        }
                        result.Add(delimiter);
                        startIndex = i + delimiter.Length;
                        i += delimiter.Length - 1; // Skip the delimiter length - 1 characters
                        break;
                    }
                }
            }

            if (startIndex < input.Length)
            {
                result.Add(input.Substring(startIndex));
            }

            return result.ToArray();
        }

        private static Token[] TokenizeString(this string[] input)
        {
            Token[] tokens = new Token[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                string str = input[i];
                switch (str)
                {
                    case "+":
                        tokens[i] = new Token(TokenType.PLUS, str);
                        break;

                    case "-":
                        tokens[i] = new Token(TokenType.MINUS, str);
                        break;

                    case "adv":
                        tokens[i] = new Token(TokenType.ADV, str);
                        break;

                    case "eadv":
                        tokens[i] = new Token(TokenType.ADV, str);
                        break;

                    case "dis":
                        tokens[i] = new Token(TokenType.DIS, str);
                        break;

                    case "kh":
                        tokens[i] = new Token(TokenType.KEEP_H, str);
                        break;

                    case "kl":
                        tokens[i] = new Token(TokenType.KEEP_L, str);
                        break;

                    case "d":
                        tokens[i] = new Token(TokenType.DIE, str);
                        break;

                    case " ":
                        tokens[i] = new Token(TokenType.WHITE_S, str);
                        break;

                    case "-t":
                        tokens[i] = new Token(TokenType.TARGET, str);
                        break;

                    case "-c":
                        tokens[i] = new Token(TokenType.COMMENT, str);
                        break;

                    default:
                        if (int.TryParse(str, out int num))
                        {
                            tokens[i] = new Token(TokenType.NUMBER, num.ToString());
                        }
                        else
                        {
                            tokens[i] = new Token(TokenType.STRING, str);
                        }
                        break;
                }
            }

            return tokens;
        }

        public static string GetTokenDescription(this TokenType type) => type switch
        {
            TokenType.NUMBER => "Number",
            TokenType.STRING => "String",
            TokenType.DIE => "Dice",
            TokenType.KEEP_H => "Keep Highest",
            TokenType.KEEP_L => "Keep Lowest",
            TokenType.ADV => "Advantage",
            TokenType.DIS => "Disadvantage",
            TokenType.WHITE_S => "WhiteSpace",
            TokenType.PLUS => "Addition",
            TokenType.MINUS => "Subtraction",
            TokenType.COMMENT => "Comment",
            TokenType.TARGET => "Target",
            _ => throw new NotSupportedException(type.ToString())
        };
    }
}
