
public abstract class Expression : ASTNode
{
    public Expression(CodeLocation location) : base (location) { }

    public abstract ExpressionType Type { get; set; }

    public abstract object? Value { get; set; }

    public abstract void Evaluate();
}
