using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnoNet.Server.Utils
{
    internal class ClientManager
    {
        internal static List<Client> clients = new List<Client>();
        internal static async Task listenForClientConnections(TcpListener listener, CancellationToken ct) {
            while (!ct.IsCancellationRequested) {
                TcpClient client = await listener.AcceptTcpClientAsync();
                Server.InvokeOnClientConnects(new ClientConnectionArgs(client));
                clients.Add(new Client(client));
            }
        }

        internal static void removeClient(int id) {
            foreach (Client client in clients) {
                if (client.ID == id) {
                    client.ctSource.Cancel();
                    clients.Remove(client);
                    break;
                }
            }
        }
    }
}
