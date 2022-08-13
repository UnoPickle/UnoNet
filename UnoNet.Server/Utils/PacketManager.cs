using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnoNet.Server.Utils
{
    internal class PacketManager
    {
        internal static async Task listenToClient(Client client, CancellationToken ct)
        {
            TcpClient tcpClient = client.TcpClient;
            using (tcpClient)
            {
                NetworkStream stream = tcpClient.GetStream();
                byte[] buffer = new byte[tcpClient.ReceiveBufferSize];
                byte[] result;
                while (!ct.IsCancellationRequested)/*add a check to when a client disconnects*/
                {
                    result = new byte[buffer.Length];
                    await stream.ReadAsync(result, 0, (int)buffer.Length);
                    if(result != null) Server.InvokeOnPacketRecieved(new RecievedPacketData(result, client));
                }
            }
        }
    }
}
