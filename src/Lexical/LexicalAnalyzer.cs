
public class Compiling
{
    private static LexicalAnalyzer? __LexicalProcess;
    public static LexicalAnalyzer Lexical
    {
        get
        {
            if (__LexicalProcess == null)
            {
                __LexicalProcess = new LexicalAnalyzer();


                __LexicalProcess.RegisterOperator("+", TokenValues.Add);
                __LexicalProcess.RegisterOperator("*", TokenValues.Mul);
                __LexicalProcess.RegisterOperator("-", TokenValues.Sub);
                __LexicalProcess.RegisterOperator("/", TokenValues.Div);
                __LexicalProcess.RegisterOperator("=", TokenValues.Assign);

                __LexicalProcess.RegisterOperator(",", TokenValues.ValueSeparator);
                __LexicalProcess.RegisterOperator(";", TokenValues.StatementSeparator);
                __LexicalProcess.RegisterOperator("(", TokenValues.OpenBracket);
                __LexicalProcess.RegisterOperator(")", TokenValues.ClosedBracket);
                __LexicalProcess.RegisterOperator("{", TokenValues.OpenCurlyBraces);
                __LexicalProcess.RegisterOperator("}", TokenValues.ClosedCurlyBraces);

                __LexicalProcess.RegisterKeyword("Card", TokenValues.Card);
                __LexicalProcess.RegisterKeyword("Element", TokenValues.Element);
                __LexicalProcess.RegisterKeyword("power", TokenValues.power);
                __LexicalProcess.RegisterKeyword("elements", TokenValues.elements);                
                __LexicalProcess.RegisterKeyword("id", TokenValues.id);
                __LexicalProcess.RegisterKeyword("weak", TokenValues.weak);
                __LexicalProcess.RegisterKeyword("strong", TokenValues.strong);

                __LexicalProcess.RegisterText("\"", "\"");
            }

            return __LexicalProcess;
        }
    }
}