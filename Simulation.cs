
public class Ring{
    public void Fight(ElementalProgram program, Card player1, Card player2)
    {
        double pow1 = (double)player1.Power.Value;
        double pow2 = (double)player2.Power.Value;

        foreach (string element1 in player1.cardElements)
        {
            foreach (string element2 in player2.cardElements)
            {
                int factor = Calculate(program, element1, element2);
                pow1 += factor * 20;
                pow2 -= factor * 20;
            }
        }

        Console.WriteLine("{0} has {1} power", player1.Id, pow1);
        Console.WriteLine("{0} has {1} power", player2.Id, pow2);
        string winner;
        string loser;
        if (pow1 > pow2)
        {
            winner = player1.Id;
            loser = player2.Id;
        }
        else
        {
            winner = player2.Id;
            loser = player1.Id;
        }

        Random random = new Random();

        int i = random.Next(5);
        if (i == 0)
            Console.WriteLine("{0} kicked {1}'s ass", winner, loser);
        if (i == 1)
            Console.WriteLine("{1} is crying", winner, loser);
        if (i == 2)
            Console.WriteLine("{0} is taking a nap over {1}'s dead body", winner, loser);
        if (i == 3)
            Console.WriteLine("{1} ran away from {0}", winner, loser);
        if (i == 4)
            Console.WriteLine("{0} killed {1}", winner, loser);
    }

    private int Calculate(ElementalProgram program, string element1, string element2)
    {
        int result = 0;
        

        if(program.Elements[element1].Weak.Contains(element2))
        {
            result -= 1;
        }
        if(program.Elements[element1].Strong.Contains(element2))
        {
            result += 1;
        }
        if(program.Elements[element2].Weak.Contains(element1))
        {
            result += 1;
        }
        if(program.Elements[element2].Weak.Contains(element1))
        {
            result -= 1;
        }

        return result;
    }
}
