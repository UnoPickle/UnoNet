using UnoNet.Core;

namespace UnoNet.Client.Utils
{
    /// <summary>
    /// Data that passes when a client disconnects
    /// </summary>
    public class ClientDisconnectingArgs
    {
        /// <summary>
        /// Disconnecting client ID
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Reason of disconnection
        /// </summary>
        public DisconnectReason reason { get; }

        public ClientDisconnectingArgs(int ID, DisconnectReason reason) { 
            this.ID = ID;
            this.reason = reason;
        }
    }
}
