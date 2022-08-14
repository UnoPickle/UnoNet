using System;
using UnoNet.Client;

namespace UnoNetClientTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Client.Connect("127.0.0.1:4343");
            Console.ReadKey();
            Client.Disconnect();
            Console.ReadKey();
        }
    }
}
