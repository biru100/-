using System;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Chat
{
    static void Main(string[] args)
    {
        Chat chat = new Chat();

    }

    public Chat()
    {
        int number = 0;     // 서버인지 클라이언트인지 결정 받을 변수
        while (true)
        {
            Console.WriteLine("1. 서버, 2. 클라이언트, 3. 종료");   // 내용 출력
            Console.WriteLine("번호를 입력해주세요 : ");
            number = Console.Read() - '0';  // 숫자 값 입력
            if (number < 1 || number > 3)  // 범위 초과 시
                Console.Clear();
            else
                break;
        }
        switch (number)     // 값에 따라서 처리
        {
            case 1:
                chatserver();   // 서버
                break;
            case 2:
                chatclient();   // 클라이언트
                break;
        }
    }
    NetworkStream ns;       // 스레드가 나뉘어도 스트림을 사용할 수 있도록 함수 외부에 선언
    bool endcheck = false;  // 서버가 끊어질 때 스레드도 함께 종료 시키기 위해 체크 하는 변수
    string[] chatlog = new string[300];     // 채팅 기록을 받을 배열 (한도 300)
    int logcount = 0;       // 채팅 기록 갯수

    // 스레드 부분 위주로 설명하겠습니다.

    // 서버
    void chatserver()
    {
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
        TcpListener server = new TcpListener(ipep);
        server.Start();

        TcpClient client = server.AcceptTcpClient();
        Console.WriteLine("Connected with {0} at port {1}",
            ((IPEndPoint)client.Client.RemoteEndPoint).Address, ((IPEndPoint)client.Client.RemoteEndPoint).Port);
        ns = client.GetStream();
        StreamReader sr = new StreamReader(ns);
        StreamWriter sw = new StreamWriter(ns);

        int length;
        string data = null;
        byte[] bytes = new byte[256];
        Thread sendthread = new Thread(new ThreadStart(ThreadSendMessage));     // 메세지를 보내는 함수를 다른 스레드에서 처리
        sendthread.Start();  // 스레드 실행
        try
        {
            while ((length = ns.Read(bytes, 0, bytes.Length)) != 0) 
            {
                data = Encoding.Default.GetString(bytes, 0, length);
                chatlog[logcount++] = "상대 : " + data;  // 받은 로그 기록
                ShowChat(); // 로그 출력
            }
            endcheck = true;    // 종료 시 스레드 종료 체크 변수 활성화
            sendthread.Join();  // 스레드 종료 시까지 무한 대기
            Console.WriteLine("Disconnecting from server...");
            Console.WriteLine("Disconnected from {0}", ((IPEndPoint)client.Client.RemoteEndPoint).Address);
            sw.Close();
            sr.Close();
            ns.Close();
            client.Close();
        }
        catch (SocketException e)
        {
            Console.WriteLine("Unable to connect to server.");
            Console.WriteLine(e.ToString());
            return;
        }
    }
    void chatclient()
    {
        IPEndPoint clientAddress =
            new IPEndPoint(IPAddress.Any, 0);

        TcpClient client = new TcpClient(clientAddress);
        IPEndPoint serverAddress =
            new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);

        try
        {
            client.Connect(serverAddress);
            ns = client.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);

            int length;
            string data = null;
            byte[] bytes = new byte[256];
            Thread sendthread = new Thread(new ThreadStart(ThreadSendMessage)); // 메세지를 보내는 함수를 다른 스레드에서 처리
            sendthread.Start(); // 스레드 실행
            while ((length = ns.Read(bytes, 0, bytes.Length)) != 0)
            {
                data = Encoding.Default.GetString(bytes, 0, length);
                chatlog[logcount++] = "상대 : " + data;   // 받은 로그 기록
                ShowChat(); // 로그 출력
            }
            endcheck = true;    // 종료 시 스레드 종료 체크 변수 활성화
            sendthread.Join();// 스레드 종료 시까지 무한 대기
            Console.WriteLine("Disconnecting from server...");
            sr.Close();
            sw.Close();
            ns.Close();
            client.Close();
        }
        catch (SocketException e)
        {
            Console.WriteLine("Unable to connect to server.");
            Console.WriteLine(e.ToString());
            return;
        }
    }
    
    // 메세지를 보내는 함수
    void ThreadSendMessage()
    {
        while(!endcheck)    // 서버와 클라이언트의 연결이 끊어짐을 체크
        {
            string data = Console.ReadLine();
            if(data != "")  // 데이터 값이 있을 때만 기록
            {
                chatlog[logcount++] = "나 : " + data;   // 보내는 데이터 로그 기록
                byte[] msg = Encoding.Default.GetBytes(data);
                ns.Write(msg, 0, msg.Length);
                ShowChat();  // 로그 출력
            }
        }

    }

    // 로그 출력
    void ShowChat()
    {
        Console.Clear();    // 화면을 지워줌
        for(int i = logcount - 20; i < logcount; i++)   // 로그가 가장 최근으로부터 20개만 저장
        {
            if (i < 0)  // 로그가 20게 미만일 경우 범위를 벗어나지 않기 위해 0 고정
                i = 0;
            Console.WriteLine(chatlog[i]); // 로그 출력
        }
    }
}
