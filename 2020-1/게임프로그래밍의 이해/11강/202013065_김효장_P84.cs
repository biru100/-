using System;

class Program 
{ 
    static void Main() 
    { 
        int[] myPos = { 20, 30 };   
        // 자신이 있는 위치         
        int[,] enemyPos =           
        // 적이 있는 위치         
        {
            {2, 4 },
            {10, 6 },
            {25, 20 }
        };         
        int i;         
        int dx, dy;         
        double d;         
        double r = 15;              
        // 사정거리         
        for ( i = 0; i < 3; i++ )         
        {             
            dx = myPos[0] - enemyPos[i, 0];             
            dy = myPos[1] - enemyPos[i, 1];             
            d = (double)(dx * dx + dy * dy);             
            if ( d > r * r )             
            {                 
                Console.WriteLine("적" + i + "은(는) 사정권 밖에 있음");             
            } else             
            {                        
                Console.WriteLine("적" + i + "은(는) 사정권 안에 있음");                         
            }         
        }     
    } 
}