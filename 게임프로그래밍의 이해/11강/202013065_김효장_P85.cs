using System;

class Scores
{
    static void Main()
    {
        string[] names = { "홍길동", "김철수", "촤하나", "이길상", "권태기", "장만옥" };

        int[] scores = { 48, 75, 62, 90, 84, 28 };

        int i, j; string s;

        for (i = 0; i < 6; i++) 
        { 
            Console.Write(names[i] + " " + scores[i] + " "); 
            for (j = 0; j < scores[i] / 5; j++) 
            { 
                Console.Write("*"); 
            } 
            if (scores[i] < 50) 
                s = "조금 부족해요!"; 
            else if (scores[i] < 70) 
                s = "보통입니다."; 
            else if (scores[i] < 90) 
                s = "잘 했습니다."; 
            else 
                s = "매우 잘 했습니다!"; 
            Console.WriteLine(" " + s); 
        }
    }
}