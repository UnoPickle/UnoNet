using System;
using UnoNet.Server;
using UnoNet.Server.Utils;

namespace UnoNetServerTest
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine(Server.init(4343));
            UnoNet.Server.Server.OnPacketRecieved += Server_OnPacketRecieved;
            Server.OnClientConnects += Server_OnClientConnects;
            Console.ReadKey();
        }

        private static void Server_OnClientConnects(object sender, ClientConnectionArgs e)
        {
            Console.WriteLine(e.IP);
        }

        private static void Server_OnPacketRecieved(object sender, UnoNet.Server.Utils.RecievedPacketData data)
        {
            Console.WriteLine(data.data);
        }
    }
}
