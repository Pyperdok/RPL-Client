using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace RPL_Client
{
    class SocketClient
    {
        public static void ClientListen(Socket tcpSocket)
        {
            var buffer = new byte[256];
            var size = 0;

            var data = "";

            do
            {
                size = tcpSocket.Receive(buffer);
                data = Encoding.UTF8.GetString(buffer, 0, size);

                if (data == "close")
                    break;

            }
            while (true);

            tcpSocket.Shutdown(SocketShutdown.Both);
            tcpSocket.Close();
        }

        static void d312321(string[] args)
        {
            const string ip = "176.215.255.48";
            const int port = 27015;
            //Точка входа
            var tcpEndpoint = new IPEndPoint(IPAddress.Parse(ip), port);

            //Сокет



            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpSocket.Connect(tcpEndpoint);
            
            new Thread(() => ClientListen(tcpSocket)).Start();          
        }
    }
}
