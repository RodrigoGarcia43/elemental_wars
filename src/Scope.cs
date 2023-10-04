
public class Scope
{
    public Scope()
    {
        Elements = new List<string>();   
    }

    public Scope? Parent { get; set; }

    public List<string> Elements { get; set; }

    public Scope CreateChild()
    {
        Scope child = new Scope();
        child.Parent = this;
            
        return child;
    }
}
