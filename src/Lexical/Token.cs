
public class Token
{
    public string Value { get; private set; }
    public TokenType Type { get; private set; }
    public CodeLocation Location { get; private set; }
    public Token(TokenType type, string value, CodeLocation location)
    {
        this.Type = type;
        this.Value = value;
        this.Location = location;
    }

    public override string ToString()
    {
        return string.Format("{0} [{1}]", Type, Value);
    }
}

public struct CodeLocation
{
    public string File;
    public int Line;
    public int Column;
}


public enum TokenType
{
    Unknwon,
    Number,
    Text,
    Keyword,
    Identifier,
    Symbol
}

public class TokenValues
{
    protected TokenValues() { }

    public const string Add = "Addition"; // +
    public const string Sub = "Subtract"; // -
    public const string Mul = "Multiplication"; // *
    public const string Div = "Division"; // /

    public const string Assign = "Assign"; // =
    public const string ValueSeparator = "ValueSeparator"; // ,
    public const string StatementSeparator = "StatementSeparator"; // ;

    public const string OpenBracket = "OpenBracket"; // (
    public const string ClosedBracket = "ClosedBracket"; // )
    public const string OpenCurlyBraces = "OpenCurlyBraces"; // {
    public const string ClosedCurlyBraces = "ClosedCurlyBraces"; // }

    public const string power = "power"; // power
    public const string elements = "elements"; // elements
    public const string Card = "Card"; // Card
    public const string Element = "Element"; // Element
    public const string id = "id"; // id
    public const string weak = "weak"; // weak
    public const string strong = "strong"; // strong

}
