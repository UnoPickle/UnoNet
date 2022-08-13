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
        internal static async Task listenToClient(TcpClient client, CancellationToken ct)
        {
            using (client)
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[client.ReceiveBufferSize];
                byte[] result;
                while (!ct.IsCancellationRequested)/*add a check to when a client disconnects*/
                {
                    result = new byte[buffer.Length];
                    await stream.ReadAsync(result, 0, (int)buffer.Length);
                    Server.InvokeOnPacketRecieved(new RecievedPacketData(result));
                }
            }
        }
    }
}
