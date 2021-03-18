using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

class StreamTcpClient
{
    public static void Main()
    {
        IPEndPoint clientAddress =
            new IPEndPoint(IPAddress.Any, 0);

        TcpClient client = new TcpClient(clientAddress);
        IPEndPoint serverAddress =
            new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);

        try
        {
            client.Connect(serverAddress);
            NetworkStream ns = client.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);

            int length;
            string data = null;
            byte[] bytes = new byte[256];

            while ((length = ns.Read(bytes, 0, bytes.Length)) != 0)
            {
                data = Encoding.Default.GetString(bytes, 0, length);
                Console.WriteLine(String.Format("받은 메세지 : {0}", data));
                Console.Write("메세지를 입력하세요 : ");
                byte[] msg = Encoding.Default.GetBytes(Console.ReadLine());
                ns.Write(msg, 0, msg.Length);
            }
            Console.WriteLine("Disconnecting from server...");
            sr.Close();
            sw.Close();
            ns.Close();
            client.Close();
        }
        catch(SocketException e)
        {
            Console.WriteLine("Unable to connect to server.");
            Console.WriteLine(e.ToString());
            return;
        }
    }
}
