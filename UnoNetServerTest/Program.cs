using System;
using UnoNet.Core;
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
            Server.OnClientDisconnects += Server_OnClientDisconnects;
            Console.ReadKey();
        }

        private static void Server_OnClientDisconnects(object sender, ClientDisconnectEventArgs e)
        {
            Console.WriteLine($"Client disconnected: {e.client.ID}");
        }

        private static void Server_OnClientConnects(object sender, ClientConnectionArgs e)
        {
            Console.WriteLine($"New Connection: {e.IP} + {e.client.ID}");
            //Console.WriteLine(Server.getAllClients().Count +1);
        }

        private static void Server_OnPacketRecieved(object sender, RecievedPacketData data)
        {
            Console.WriteLine(data.packet.ToString());
            
        }

        //Packet packet = new Packet(new System.Collections.Generic.Dictionary<string, object>() { { "x", 3}, { "y", 4 } });
    }
}
