using System;
using UnoNet.Client;
using UnoNet.Client.Utils;
using UnoNet.Core;

namespace UnoNetClientTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
            Client.Connect("127.0.0.1", 4323);
            Client.OnPacketRecieved += Client_OnPacketRecieved;
            Client.OnNewClient += Client_OnNewClient;
            //Client.sendToAll(new Packet(new System.Collections.Generic.Dictionary<string, object>() { { "ID", Client.ID } }));
            Console.ReadKey();
            Console.WriteLine(Client.ID);
            Client.Disconnect();
            Console.ReadKey();
        }

        private static void Client_OnNewClient(object sender, NewClientArgs e)
        {
            Console.WriteLine("Hello {0}! ",e.ID);
        }

        private static void Client_OnPacketRecieved(object sender, Packet e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}
