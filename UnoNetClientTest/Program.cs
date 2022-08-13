using System;

namespace UnoNetClientTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UnoNet.Client.Client.Connect("127.0.0.1:4343");
            Console.ReadKey();
        }
    }
}
