
public class Element : ASTNode
{
    public string Id {get; set;}
    public List<string> Weak {get; set;}
    public List<string> Strong {get; set;}

    public Element(string id, CodeLocation location) : base (location)
    {
        this.Id = id;
        Weak = new List<string>();
        Strong = new List<string>();
    }

    public bool CollectElements(Context context, Scope scope, List<CompilingError> errors)
    {
        if (context.elements.Contains(Id))
        {
            errors.Add(new CompilingError(Location, ErrorCode.Invalid, "Element already defined"));
            return false;
        }
        else
        {
            context.elements.Add(Id);
        }
        return true;
    }

    public override bool CheckSemantic(Context context, Scope scope, List<CompilingError> errors)
    {
        
        bool checkWeak = true;
        foreach (string element in Weak)
        {
            if (!context.elements.Contains(element))
            {
                errors.Add(new CompilingError(Location, ErrorCode.Invalid, String.Format("{0} element does not exists", element)));
                checkWeak = false;
            }
            if (scope.elements.Contains(element))
            {
                errors.Add(new CompilingError(Location, ErrorCode.Invalid, String.Format("{0} element already in use", element)));
                checkWeak = false;
            }
            else
            {
                scope.elements.Add(element);
            }
        }


        bool checkStrong = true;
        foreach (string element in Strong)
        {
            if (!context.elements.Contains(element))
            {
                errors.Add(new CompilingError(Location, ErrorCode.Invalid, String.Format("{0} element does not exists", element)));
                checkStrong = false;
            }
            if (scope.elements.Contains(element))
            {
                errors.Add(new CompilingError(Location, ErrorCode.Invalid, String.Format("{0} element already in use", element)));
                checkStrong = false;
            }
            else
            {
                scope.elements.Add(element);
            }
        }
        return checkWeak && checkStrong;
    }

    public override string ToString()
    {
        return String.Format("Element {0} \n\t {1} weaknesses \n\t {2} strengths", Id, Weak.Count, Strong.Count);
    }

}
