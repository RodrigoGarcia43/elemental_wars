
public abstract class ASTNode
{
    public ASTNode(CodeLocation location)
    {
        Location = location;
    }

    public CodeLocation Location {get; set;}
    
    public abstract bool CheckSemantic(Context context, Scope scope, List<CompilingError> errors);    
}
