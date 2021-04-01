using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

class StreamTcpSrvr
{
    static void Main(string[] args)
    {
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
        TcpListener server = new TcpListener(ipep);
        server.Start();

        TcpClient client = server.AcceptTcpClient();
        Console.WriteLine("Connected with {0} at port {1}",
            ((IPEndPoint)client.Client.RemoteEndPoint).Address, ((IPEndPoint)client.Client.RemoteEndPoint).Port);
        NetworkStream ns = client.GetStream();
        StreamReader sr = new StreamReader(ns);
        StreamWriter sw = new StreamWriter(ns);

        int length;
        string data = null;
        byte[] bytes = new byte[256];
        data = "안녕하세요";
        byte[] msg1 = Encoding.Default.GetBytes(data);

        ns.Write(msg1, 0, msg1.Length);
        try
        { 
            while ((length = ns.Read(bytes, 0, bytes.Length)) != 0)
            {
                data = Encoding.Default.GetString(bytes, 0, length);
                Console.WriteLine(String.Format("받은 메세지 : {0}", data));
                Console.Write("메세지를 입력하세요 : ");
                byte[] msg = Encoding.Default.GetBytes(Console.ReadLine());
                ns.Write(msg, 0, msg.Length);
            }
            Console.WriteLine("Disconnecting from server...");
        }
        catch (SocketException e)
        {
            Console.WriteLine("Unable to connect to server.");
            Console.WriteLine(e.ToString());
            return;
        }
        Console.WriteLine("Disconnected from {0}", ((IPEndPoint)client.Client.RemoteEndPoint).Address);
        sw.Close();
        sr.Close();
        ns.Close();
        client.Close();
    }
}
