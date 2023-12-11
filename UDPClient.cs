using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class UDPClient {
    private const int port = 7777;

    public void Start() {
        UdpClient udpClient = new UdpClient();

        IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);

        while (true) {
            byte[] receiveBytes = udpClient.Receive(ref serverEP);
            string receiveMessage = Encoding.ASCII.GetString(receiveBytes);

            Console.WriteLine("서버로부터 메시지 수신: " + receiveMessage);
        }
    }
}
