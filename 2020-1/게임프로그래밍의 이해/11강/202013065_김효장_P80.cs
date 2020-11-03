using System;

class Break
{
    static void Main()
    {
        int a, b = 2;

        for (a = 0; a < 5; a++)
        {
            if (b - a <= 0)
                break;
            Console.WriteLine(b + " - " + a + " = " + (b - a));
        }
    }
}