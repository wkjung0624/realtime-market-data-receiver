﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    public class UDPClient
    {
        private IPEndPoint ipEndPoint;
        private int port;
        private Socket socket;

        /**
            * 서버 요청 payload *
            ------------------------------------------------
            1: 요청헤더, 2-1025: 상세내용, 1026-4096: 미정
            ------------------------------------------------
            - 요청헤더 
            0x01 : 서버 접속 / 서버 등록
            0x10 : 실시간 시세 조회 요청 등록
            0x20 : 실시간 시세 조회 요청 해제
            0xFF : 접속 종료 / 서버 등록 해제요청
        */
        private const byte SERVER_REG_PAYLOAD     = 0x01;
        private const byte RT_PRICE_SUB_PAYLOAD   = 0x10;
        private const byte RT_PRICE_UNSUB_PAYLOAD = 0x20;
        private const byte SERVER_DEREG_PAYLOAD   = 0xFF;
        
        public void Initialize(string ip="127.0.0.1", int port=5555)
        {
            Console.WriteLine("클라이언트를 초기화 합니다."); 

            ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }
        
        public void Subscribe(string serverIp="127.0.0.1", int port=5555, string ticker="")
        {
            byte[] payload = new byte[4096];

            payload[0] = SERVER_REG_PAYLOAD;
            // payload[1-1024] = ticker

            Console.WriteLine("서버에 연결 등록");

            socket.SendTo(payload, payload.Length, SocketFlags.None, ipEndPoint);
        }
        public void StartMessageReceiveLoop()
        {
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint remote = (EndPoint)(sender);

            byte[] recvData = new byte[4096];

            Console.WriteLine("서버로부터 메시지 수신 대기중");

            while (true)
            {
                // ReceiveFrom()
                socket.ReceiveFrom(recvData, ref remote);
                Console.WriteLine("Client Receive Data : {0}", Encoding.Default.GetString(recvData));
            }
        }
        public void UnSubscribe() 
        {
            /* 구현필요부분 */
            Console.WriteLine("미구현");
        }
        
        public void Close()
        {
            socket.Close();
            Console.WriteLine("서버와의 연결 종료");
            Console.ReadLine();
        }
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            UDPClient client = new UDPClient();

            client.Initialize();
            client.Subscribe();
            client.StartMessageReceiveLoop();
        }
    }
}

// 참조: https://jinjae.tistory.com/50