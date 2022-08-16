using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnoNet.Core;

namespace UnoNet.Server.Utils
{
    /// <summary>
    /// Data the gets passed when a new client connects
    /// </summary>
    public class ClientConnectionArgs
    {
        internal TcpClient TcpClient { get; }

        /// <summary>
        /// IP of the client
        /// </summary>
        public string IP { get; }

        /// <summary>
        /// Client class of the connecting client
        /// </summary>
        public Client client { get; }

        public ClientConnectionArgs(Client client) {
            this.client = client;
            this.TcpClient = client.TcpClient;
            IP = ((IPEndPoint)TcpClient.Client.RemoteEndPoint).Address.ToString();
        }
    }
}
