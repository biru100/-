using System;
using System.Threading;

class ThreadGame
{
    static void Main(string[] args)
    {
        ThreadGame tg = new ThreadGame();
    }

    public ThreadGame()
    {
        // 2개의 스레드를 생성
        Thread gameThread = new Thread(new ThreadStart(quiz));       // 퀴즈의 정답을 입력하는 스레드
        Thread renderThread = new Thread(new ThreadStart(time));     // 남은 시간을 출력하는 스레드

        // 각 스레드를 실행
        gameThread.Start();
        renderThread.Start();

        // 스레드가 종료될 때까지 무한 대기
        gameThread.Join();
        renderThread.Join();
    }

    // 게임의 시간
    int gametime = 5;

    // 퀴즈 로직 함수
    void quiz()
    {
        int i = Console.Read() - '0';   // 숫자 값 입력
        if (gametime > 0)  // 시간 초과 시 입력 불가
        {
            if (i == 2)   // 정답 체크
                Console.WriteLine("정답!");
            else
                Console.WriteLine("오답...");
            gametime = -1;   // 문제를 무사히 풀었을 경우 gametime은 -1
        }

        Console.ReadLine();
    }

    // 시간 출력 함수
    void time()
    {
        for(gametime = 5; gametime > 0; gametime--) // 게임타임 소진 시 까지 반복 (게임타임 5초)
        {
            Console.Clear();   // 화면 지우기
            Console.WriteLine("1 + 1 = ?");  // 퀴즈 출력
            Console.WriteLine(gametime.ToString() + "초 남았습니다.");    // 남은 시간 출력
            Thread.Sleep(1000);   // 대기
        }
        if(gametime == 0)   // 시간 초과 시 출력
        {
            Console.Clear();
            Console.WriteLine("시간 초과...");
        }
    }
}

