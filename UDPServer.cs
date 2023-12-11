using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UDPServer
{
    private const int port = 7777;
    private UdpClient udpServer;
    private IPEndPoint clientEP;

    public void Start()
    {
        udpServer = new UdpClient(port);
        clientEP = new IPEndPoint(IPAddress.Any, 0);

        Console.WriteLine("UDP 서버 시작. 클라이언트 메시지 수신 대기 중...");

        // 클라이언트에게 매 초마다 "hello" 문자열을 전달하는 쓰레드 시작
        Thread sendThread = new Thread(SendHelloToClient);
        sendThread.Start();
    }

    private void SendHelloToClient()
    {
        while (true) {
            byte[] helloBytes = Encoding.ASCII.GetBytes("hello");
            udpServer.Send(helloBytes, helloBytes.Length, clientEP);
            Console.WriteLine("클라이언트에게 'hello' 전송");

            Thread.Sleep(1000); // 1초 대기 후 다시 전송
        }
    }
}