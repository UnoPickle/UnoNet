
using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using UnoNet.Core;

namespace UnoNet.Server
{
    public class Client
    {
        public readonly int ID;
        internal TcpClient TcpClient { set; get; }
        internal CancellationToken ct { get; }
        internal CancellationTokenSource ctSource { get; }

        internal void Close() { 
            ctSource.Cancel();
            TcpClient.Close();
        }

        internal Client(TcpClient client) {
            Random rnd = new Random();
            int rndID = rnd.Next(10000, 99999);
            while (!Utils.ClientManager.checkIDAvailiblity(rndID)) { 
                rndID = rnd.Next(10000, 99999);
            }
            ID = rndID;
            this.TcpClient = client;
            ctSource = new CancellationTokenSource();
            ct = ctSource.Token;
            var task = Utils.PacketManager.listenToClient(this);
        }

    }
}
