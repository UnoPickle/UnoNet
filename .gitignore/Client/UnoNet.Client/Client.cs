using System;
using System.Net.Sockets;

namespace UnoNet.Client
{
    public static class Client{
        public static TcpClient client;

        public static bool Connect(string ServerIP) {
            IP ip = IP.splicePort(ServerIP);
            client = new TcpClient();
            try
            {
                if (ip.Port == 0 || ip.Port == null) ip.Port = Defaults.Port;  
                client.Connect(ip.getIP(), ip.Port);
                return true;
            }
            catch (Exception e) { return false; }
        }

        public static void Disconnect() {
            client.GetStream().Dispose();
            client.Close();
            client = null;
        }
    }
}
