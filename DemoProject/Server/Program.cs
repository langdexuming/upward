using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //var server = Dns.GetHostName();
            var server = "127.0.0.1";

            var port = 5200;

            Socket s = null;

            // IPHostEntry hostEntry = null;

            // // Get host related information.
            // hostEntry = Dns.GetHostEntry(server);

            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Bind(new IPEndPoint(IPAddress.Parse(server), port));
            s.Listen(1);
            var csFd = s.Accept();
            Console.WriteLine(csFd.RemoteEndPoint.ToString());

            byte[] buffer = new byte[2500];
            csFd.Receive(buffer);
            var str = Encoding.ASCII.GetString(buffer);
            Console.WriteLine(str);

            Console.ReadLine();

            s.Shutdown(SocketShutdown.Both);
        }
    }
}
