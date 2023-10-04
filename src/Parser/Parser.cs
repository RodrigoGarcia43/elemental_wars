
public class Parser
{
    public Parser(TokenStream stream)
    {
        Stream = stream;
    }

    public TokenStream Stream { get; private set; }

    public ElementalProgram ParseProgram(List<CompilingError> errors)
    {
        ElementalProgram program = new ElementalProgram(new CodeLocation());

        if (!Stream.CanLookAhead(0)) return program;

        while (Stream.LookAhead().Value == TokenValues.Element)
        {
            Element element = ParseElement(errors);
            program.Elements[element.Id] = element;

            if (!Stream.Next(TokenValues.StatementSeparator))
            {
                errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "; expected"));
                return program;
            }

            if (!Stream.Next()) break;
        }
        
        while (Stream.LookAhead().Value == TokenValues.Card)
        {
            Card card = ParseCard(errors);
            program.Cards[card.Id] = card;

            if (!Stream.Next(TokenValues.StatementSeparator))
            {
                errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "; expected"));
                return program;
            }

            if (!Stream.Next()) break;
        }
        return program;
    }

    public Element ParseElement(List<CompilingError> errors)
    {
        Element element = new Element("null", Stream.LookAhead().Location);
        
        if(!Stream.Next(TokenType.Identifier))
        {
            errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "id expected"));
        }
        element.Id = Stream.LookAhead().Value;

        if(!Stream.Next(TokenValues.OpenCurlyBraces))
        {
            errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "{ expected"));
        }
        
        if(!Stream.Next(TokenValues.weak))
        {
            errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "weak expected"));
        }
        if(!Stream.Next(TokenValues.Assign))
        {
            errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "= expected"));
        }
        while(Stream.Next(TokenType.Identifier))
        {
            element.Weak.Add(Stream.LookAhead().Value);
        }
        if(!Stream.Next(TokenValues.StatementSeparator))
        {
            errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "; expected"));
        }
        
        if(!Stream.Next(TokenValues.strong))
        {
            errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "strong expected"));
        }
        if(!Stream.Next(TokenValues.Assign))
        {
            errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "= expected"));
        }
        
        while(Stream.Next(TokenType.Identifier))
        {
            element.Strong.Add(Stream.LookAhead().Value);
        }
        if(!Stream.Next(TokenValues.StatementSeparator))
        {
            errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "; expected"));
        }
        if(!Stream.Next(TokenValues.ClosedCurlyBraces))
        {
            errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "} expected"));
        }
        return element;
    }

    public Card ParseCard(List<CompilingError> errors)
    {
        Card card = new Card("null", Stream.LookAhead().Location);

        if(!Stream.Next(TokenType.Identifier))
        {
            errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "id expected"));
        }
        else
        {
            card.Id = Stream.LookAhead().Value;
        }
        if(!Stream.Next(TokenValues.OpenCurlyBraces))
        {
            errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "{ expected"));
        }
        
        if(!Stream.Next(TokenValues.power))
        {
            errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "power expected"));
        }
        if(!Stream.Next(TokenValues.Assign))
        {
            errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "= expected"));
        }
        
        Expression? exp = ParseExpression();
        if(exp == null)
        {
            errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Invalid, "Bad expression"));
            return card;
        }
        card.Power = exp;
        
        if(!Stream.Next(TokenValues.StatementSeparator))
        {
            errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "; expected"));
        }
        if(!Stream.Next(TokenValues.elements))
        {
            errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "elements expected"));
        }
        if(!Stream.Next(TokenValues.Assign))
        {
            errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "= expected"));
        }
        while(Stream.Next(TokenType.Identifier))
        {
            card.Elements.Add(Stream.LookAhead().Value);
        }
        if(!Stream.Next(TokenValues.StatementSeparator))
        {
            errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "; expected"));
        }
        if(!Stream.Next(TokenValues.ClosedCurlyBraces))
        {
            errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "} expected"));
        }
        
        return card;
    }

    private Expression? ParseExpression()
    {
        return ParseExpressionLv1();
    }

   private Expression? ParseExpressionLv1()
    {
        Expression? newLeft = ParseExpressionLv2();
        Expression? exp = ParseExpressionLv1_(newLeft);
        return exp;
    }

    private Expression? ParseExpressionLv1_(Expression? left)
    {
        Expression? exp = ParseAdd(left);
        if(exp != null) return exp;

        exp = ParseSub(left);
        if(exp != null) return exp;

        return left;
    }

    private Expression? ParseExpressionLv2()
    {
        Expression? newLeft = ParseExpressionLv3();
        return ParseExpressionLv2_(newLeft);
    }

    private Expression? ParseExpressionLv2_(Expression? left)
    {
        Expression? exp = ParseMul(left);
        if(exp != null) return exp;

        exp = ParseDiv(left);
        if(exp != null) return exp;

        return left;
    }

    private Expression? ParseExpressionLv3()
    {
        Expression? exp = ParseNumber();
        if(exp != null) return exp;

        exp = ParseText();
        if(exp != null) return exp;

        return null;
    }

    private Expression? ParseAdd(Expression? left)
    {
        Add sum = new Add(Stream.LookAhead().Location);

        if (left == null || !Stream.Next(TokenValues.Add)) return null;
        
        sum.Left = left;

        Expression? right = ParseExpressionLv2();
        if(right == null)
        {
            Stream.MoveBack(2);
            return null;
        }
        sum.Right = right;

        return ParseExpressionLv1_(sum);
    }

    private Expression? ParseSub(Expression? left)
    {
        Sub sub = new Sub(Stream.LookAhead().Location);

        if (left == null || !Stream.Next(TokenValues.Sub)) return null;
        
        sub.Left = left;

        Expression? right = ParseExpressionLv2();
        if(right == null)
        {
            Stream.MoveBack(2);
            return null;
        }
        sub.Right = right;

        return ParseExpressionLv1_(sub);
    }

    private Expression? ParseMul(Expression? left)
    {
        Mul mul = new Mul(Stream.LookAhead().Location);

        if (left == null || !Stream.Next(TokenValues.Mul)) return null;
        
        mul.Left = left;

        Expression? right = ParseExpressionLv3();
        if(right == null)
        {
            Stream.MoveBack(2);
            return null;
        }
        mul.Right = right;

        return ParseExpressionLv2_(mul);
    }

    private Expression? ParseDiv(Expression? left)
    {
        Div div = new Div(Stream.LookAhead().Location);

        if (left == null || !Stream.Next(TokenValues.Div)) return null;
        
        div.Left = left;

        Expression? right = ParseExpressionLv3();
        if(right == null)
        {
            Stream.MoveBack(2);
            return null;
        }
        div.Right = right;

        return ParseExpressionLv2_(div);
    }

    private Expression? ParseNumber()
    {
        if (!Stream.Next(TokenType.Number)) return null;

        return new Number(double.Parse(Stream.LookAhead().Value), Stream.LookAhead().Location);
    }

    private Expression? ParseText()
    {
        if (!Stream.Next(TokenType.Text)) return null;

        return new Text(Stream.LookAhead().Value, Stream.LookAhead().Location);
    }
}
