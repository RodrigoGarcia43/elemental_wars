
public class ElementalProgram : ASTNode
{
    public ElementalProgram(CodeLocation location) : base (location)
    {
        Errors = new List<CompilingError>();
        Elements = new Dictionary<string, Element>();
        Cards = new Dictionary<string, Card>();
    }

    public List<CompilingError> Errors { get; set; }

    public Dictionary<string, Element> Elements { get; set; }

    public Dictionary<string, Card> Cards { get; set; }

    public override bool CheckSemantic(Context context, Scope scope, List<CompilingError> errors)
    {
        bool checkElements = true;
        foreach (Element element in Elements.Values)
        {
            checkElements = checkElements && element.CollectElements(context, scope.CreateChild(), errors);
        }
        foreach (Element element in Elements.Values)
        {
            checkElements = checkElements && element.CheckSemantic(context, scope.CreateChild(), errors);
        }

        bool checkCards = true;
        foreach (Card card in Cards.Values)
        {
            checkCards = checkCards && card.CheckSemantic(context, scope, errors);
        }

        return checkCards && checkElements;
    }

    public void Evaluate()
    {
        foreach (Card card in Cards.Values)
            card.Evaluate();
    }

    public override string ToString()
    {
        string s = "";
        foreach (Element element in Elements.Values)
        {
            s = s + "\n" + element.ToString();
        }
        foreach (Card card in Cards.Values)
        {
            s += "\n" + card.ToString();
        }
        return s;
    }
}
