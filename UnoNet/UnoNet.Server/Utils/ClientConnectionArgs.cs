using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UnoNet.Server.Utils
{
    public class ClientConnectionArgs
    {
        internal TcpClient client { get; }
        public string IP { get; }

        public ClientConnectionArgs(TcpClient client) { 
            this.client = client;
            IP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
        }
    }
}
