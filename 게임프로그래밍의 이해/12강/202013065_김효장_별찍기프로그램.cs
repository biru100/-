using System;

namespace star
{
    class Program
    {
        static void Main(string[] args)
        {
            int value = 5;
            for (int n = 0; n < value; n++)
            {
                for (int i = 0; i < value; i++)
                {
                    for (int j = 0; j < value; j++)
                    {
                        for (int k = 0; k < value - i; k++)
                        {
                            if ((j + n) % 2 == 0)
                                Console.Write("*");
                            else
                                Console.Write(" ");
                        }
                        for (int k = 0; k < i * 2 + 1; k++)
                        {
                            if ((j + n) % 2 == 0)
                                Console.Write(" ");
                            else
                                Console.Write("*");
                        }
                        for (int k = 0; k < value - i; k++)
                        {
                            if ((j + n) % 2 == 0)
                                Console.Write("*");
                            else
                                Console.Write(" ");
                        }
                    }
                    Console.WriteLine(" ");
                }
                for (int i = 0; i < value - 1; i++)
                {
                    for (int j = 0; j < value; j++)
                    {
                        for (int k = 0; k < i + 2; k++)
                        {
                            if ((j + n) % 2 == 0)
                                Console.Write("*");
                            else
                                Console.Write(" ");
                        }
                        for (int k = 0; k < (value - i - 1) * 2 - 1; k++)
                        {
                            if ((j + n) % 2 == 0)
                                Console.Write(" ");
                            else
                                Console.Write("*");
                        }
                        for (int k = 0; k < i + 2; k++)
                        {
                            if ((j + n) % 2 == 0)
                                Console.Write("*");
                            else
                                Console.Write(" ");
                        }
                    }
                    Console.WriteLine(" ");
                }
            }
        }
    }
}
