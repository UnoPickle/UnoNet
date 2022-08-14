using System;
using System.Collections.Generic;
using System.Text;
using UnoNet.Core;

namespace UnoNet.Server.Utils
{
    public class ClientDisconnectEventArgs
    {
        public Client client { get; }
        public DisconnectReason reason { get;  }
        internal ClientDisconnectEventArgs(Client client, DisconnectReason reason) {
            this.client = client;
            this.reason = reason;
        }
    }
}
