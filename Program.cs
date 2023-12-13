using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace UDP_CLIENT
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program();
        }
        public Program()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5555);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            // string --> byte[]
            byte[] _data = Encoding.Default.GetBytes("Server SendTo Data");

            // Connect() 후 Send() 가능

            // SendTo()
            client.SendTo(_data, _data.Length, SocketFlags.None, ipep);

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint remote = (EndPoint)(sender);

            _data = new byte[1024];

            // ReceiveFrom()
            client.ReceiveFrom(_data, ref remote);
            Console.WriteLine("{0} : \r\nClient Receive Data : {1}", remote.ToString(), 
                Encoding.Default.GetString(_data));

            // Close()
            client.Close();

            Console.WriteLine("Press Any key...");
            Console.ReadLine();
        }

    }
}