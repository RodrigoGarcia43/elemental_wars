
public class Card : ASTNode
{
    public Card(string id, CodeLocation location) : base (location)
    {
        Elements = new List<string>();
        this.Id = id;
    }

    public string Id { get; set; }

    public Expression? Power { get; set; }

    public List<string> Elements { get; set; }

    public override bool CheckSemantic(Context context, Scope scope, List<CompilingError> errors)
    {
        bool checkPower = Power!.CheckSemantic(context, scope, errors);
        if (Power.Type != ExpressionType.Number)
        {
            errors.Add(new CompilingError(Location, ErrorCode.Invalid, "The Power must be numerical"));
        }

        bool checkElements = true;
        foreach (string element in Elements)
        {
            if (!context.Elements.Contains(element))
            {
                errors.Add(new CompilingError(Location, ErrorCode.Invalid, String.Format("{0} element Does not exists", element)));
                checkElements = false;
            }
            if (scope.Elements.Contains(element))
            {
                errors.Add(new CompilingError(Location, ErrorCode.Invalid, String.Format("{0} element already in use", element)));
                checkElements = false;
            }
            else
            {
                scope.Elements.Add(element);
            }
        }

        return checkPower && checkElements;
    }

    public void Evaluate() => Power!.Evaluate();

    public override string ToString() 
        => String.Format("Card {0} \n\t Power: {1} \n\t elements: {2}", Id, Power, Elements.Count);
}
