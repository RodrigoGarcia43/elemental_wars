
LexicalAnalyzer lex = Compiling.Lexical;

string text = File.ReadAllText("./examples/code");  

IEnumerable<Token> tokens = lex.GetTokens("code", text, new List<CompilingError>());

// foreach (Token token in tokens)
// {
//     Console.WriteLine(token);
// }

TokenStream stream = new TokenStream(tokens);
Parser parser = new Parser(stream);
List<CompilingError> errors = new List<CompilingError>();

ElementalProgram program = parser.ParseProgram(errors);

// Console.WriteLine(program);

if (errors.Count > 0)
{
    foreach (CompilingError error in errors)
    {
        Console.WriteLine("{0}, {1}, {2}", error.Location.Line, error.Code, error.Argument);
    }
}
else
{
    Context context = new Context();
    Scope scope = new Scope();

    program.CheckSemantic(context, scope, errors);

    if (errors.Count > 0)
    {
        foreach (CompilingError error in errors)
        {
            Console.WriteLine("{0}, {1}, {2}", error.Location.Line, error.Code, error.Argument);
        }
    }
    else
    {
        program.Evaluate();

        Console.WriteLine(program);


        Ring ring = new Ring();
        ring.Fight(program, program.Cards["Zuko"], program.Cards["Katara"]);
    }
}
