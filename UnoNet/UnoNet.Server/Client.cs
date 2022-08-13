
using System.Net.Sockets;
using System.Threading;

namespace UnoNet.Server
{
    public class Client
    {
        public readonly int ID;
        internal TcpClient client { set; get; }
        internal CancellationToken ct { get; }
        internal CancellationTokenSource ctSource { get; }

        public Client(TcpClient client) { 
            //Generates random id
            this.client = client;
            ctSource = new CancellationTokenSource();
            ct = ctSource.Token;
            var task = Utils.PacketManager.listenToClient(this.client, ct);
        }

    }
}
