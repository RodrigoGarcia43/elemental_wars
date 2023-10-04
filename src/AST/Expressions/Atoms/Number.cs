public class Number : AtomExpression
{
    public bool IsInt
    {
        get
        {
            int a;
            return int.TryParse(Value.ToString(), out a);
        }
    }

    public override ExpressionType Type
    {
        get
        {
            return ExpressionType.Number;
        }
        set { }
    }

    public override object? Value { get; set; }
    
    public Number(double value, CodeLocation location) : base(location)
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