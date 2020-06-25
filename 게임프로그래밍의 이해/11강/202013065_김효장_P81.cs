using System;

class Continue
{
    static void Main()
    {
        int a, b = 1;

        for (a = 0; a < 4; a++) 
        { 
            if (a + b == 2) 
                continue; 
            Console.WriteLine(a + " + " + b + " = " + (a + b)); 
        }
    }
}