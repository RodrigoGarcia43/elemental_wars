public class Text : AtomExpression
{

    public override ExpressionType Type
    {
        get
        {
            return ExpressionType.Text;
        }
        set { }
    }

    public override object? Value { get; set; }
    
    public Text(string value, CodeLocation location) : base(location)
    {
        Value = value;
    }
    
    public override bool CheckSemantic(Context context, Scope table, List<CompilingError> errors)
    {
        return true;
    }

    public override void Evaluate()
    {
        
    }

    public override string ToString()
    {
        return String.Format("{0}",Value);
    }
}