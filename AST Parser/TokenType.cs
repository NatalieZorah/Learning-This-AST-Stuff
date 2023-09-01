namespace LetsBuildAnAST.AST_Parser;

public enum TokenType
{
    /// <summary>
    /// Numbers.
    /// </summary>
    NUMBER,
    /// <summary>
    /// Dice Token.
    /// </summary>
    DIE,
    /// <summary>
    /// Keep Highest Token.
    /// </summary>
    KEEP_H,
    /// <summary>
    /// Keep Lowest Token.
    /// </summary>
    KEEP_L,
    /// <summary>
    /// Advantage token.
    /// </summary>
    ADV,
    /// <summary>
    /// Disadvantage token.
    /// </summary>
    DIS,
    /// <summary>
    /// Whitespace.
    /// </summary>
    WHITE_S,
    /// <summary>
    /// Addition token.
    /// </summary>
    PLUS,
    /// <summary>
    /// Subtraction token.
    /// </summary>
    MINUS,
    /// <summary>
    /// String Token.
    /// </summary>
    STRING,
    /// <summary>
    /// Target Token.
    /// </summary>
    TARGET,
    /// <summary>
    /// Comment Token.
    /// </summary>
    COMMENT
}
