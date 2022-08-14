using System;
using UnoNet.Client;
using UnoNet.Core;

namespace UnoNetClientTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Client.Connect("127.0.0.1:4343");
            Client.OnPacketRecieved += Client_OnPacketRecieved;
            Client.sendToAll(new Packet(new System.Collections.Generic.Dictionary<string, object>() { { "ID", Client.ID } }));
            Console.ReadKey();
            Client.Disconnect();
            Console.ReadKey();
        }

        private static void Client_OnPacketRecieved(object sender, Packet e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}
