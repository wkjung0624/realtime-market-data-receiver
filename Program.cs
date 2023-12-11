using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        UDPServer server = new UDPServer();
        UDPClient client = new UDPClient();

        // 서버를 쓰레드로 실행
        Thread serverThread = new Thread(server.Start);
        serverThread.Start();

        // 클라이언트를 실행
        client.Start();

        Console.WriteLine("프로그램 종료");
    }
}