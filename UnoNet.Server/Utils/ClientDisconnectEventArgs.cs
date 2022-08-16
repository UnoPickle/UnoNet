using System;
using System.Collections.Generic;
using System.Text;
using UnoNet.Core;

namespace UnoNet.Server.Utils
{
    /// <summary>
    /// Data that gets passed when a client disconnects
    /// </summary>
    public class ClientDisconnectEventArgs
    {
        /// <summary>
        /// Client class of the disconnecting client
        /// </summary>
        public Client client { get; }
        /// <summary>
        /// The reason of why did the client disconnected
        /// </summary>
        public DisconnectReason reason { get;  }
        internal ClientDisconnectEventArgs(Client client, DisconnectReason reason) {
            this.client = client;
            this.reason = reason;
        }
    }
}
