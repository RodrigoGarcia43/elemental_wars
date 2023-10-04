
public abstract class BinaryExpression : Expression
{
    public BinaryExpression(CodeLocation location) : base(location){}

    public Expression? Left { get; set; }

    public Expression? Right { get; set; }

    public override bool CheckSemantic(Context context, Scope scope, List<CompilingError> errors)
    {
        bool right = Right!.CheckSemantic(context, scope, errors);
        bool left = Left!.CheckSemantic(context, scope, errors);

        if (Right.Type != ExpressionType.Number || Left.Type != ExpressionType.Number)
        {
            errors.Add(new CompilingError(Location, ErrorCode.Invalid, "We don't do that here... "));
            Type = ExpressionType.ErrorType;
            return false;
        }

        Type = ExpressionType.Number;
        return right && left;
    }
}
