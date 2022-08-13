using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UnoNet.Server.Utils
{
    public class ClientConnectionArgs
    {
        internal TcpClient TcpClient { get; }
        public string IP { get; }
        public Client client { get; }

        public ClientConnectionArgs(Client client) { 
            this.client = client;
            this.TcpClient = client.TcpClient;
            IP = ((IPEndPoint)TcpClient.Client.RemoteEndPoint).Address.ToString();
        }
    }
}
