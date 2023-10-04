 public class Scope
    {
        public Scope? Parent;

        public List<string> elements;

        public Scope()
        {
            elements = new List<string>();   
        }

        public Scope CreateChild()
        {
            Scope child = new Scope();
            child.Parent = this;
               
            return child;
        }

    }