
public class Element : ASTNode
{
    public Element(string id, CodeLocation location) : base (location)
    {
        this.Id = id;
        Weak = new List<string>();
        Strong = new List<string>();
    }

    public string Id { get; set; }

    public List<string> Weak { get; set; }

    public List<string> Strong { get; set; }

    public bool CollectElements(Context context, Scope scope, List<CompilingError> errors)
    {
        if (context.Elements.Contains(Id))
        {
            errors.Add(new CompilingError(Location, ErrorCode.Invalid, "Element already defined"));
            return false;
        }
        else
        {
            context.Elements.Add(Id);
        }
        return true;
    }

    public override bool CheckSemantic(Context context, Scope scope, List<CompilingError> errors)
    {
        
        bool checkWeak = true;
        foreach (string element in Weak)
        {
            if (!context.Elements.Contains(element))
            {
                errors.Add(new CompilingError(Location, ErrorCode.Invalid, String.Format("{0} element does not exists", element)));
                checkWeak = false;
            }
            if (scope.Elements.Contains(element))
            {
                errors.Add(new CompilingError(Location, ErrorCode.Invalid, String.Format("{0} element already in use", element)));
                checkWeak = false;
            }
            else
            {
                scope.Elements.Add(element);
            }
        }

        bool checkStrong = true;
        foreach (string element in Strong)
        {
            if (!context.Elements.Contains(element))
            {
                errors.Add(new CompilingError(Location, ErrorCode.Invalid, String.Format("{0} element does not exists", element)));
                checkStrong = false;
            }
            if (scope.Elements.Contains(element))
            {
                errors.Add(new CompilingError(Location, ErrorCode.Invalid, String.Format("{0} element already in use", element)));
                checkStrong = false;
            }
            else
            {
                scope.Elements.Add(element);
            }
        }
        return checkWeak && checkStrong;
    }

    public override string ToString()
        => String.Format("Element {0} \n\t {1} weaknesses \n\t {2} strengths", Id, Weak.Count, Strong.Count);
}
