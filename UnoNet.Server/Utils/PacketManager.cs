using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnoNet.Core;

namespace UnoNet.Server.Utils
{
    internal class PacketManager
    {
        internal static async Task listenToClient(Client client)
        {
            TcpClient tcpClient = client.TcpClient;
            using (tcpClient)
            {
                NetworkStream stream = tcpClient.GetStream();
                byte[] buffer = new byte[tcpClient.ReceiveBufferSize];
                byte[] result;
                while (!client.ct.IsCancellationRequested)/*add a check to when a client disconnects*/
                {
                    result = new byte[buffer.Length];
                    await stream.ReadAsync(result, 0, (int)buffer.Length);
                    if(result != null) Server.InvokeOnPacketRecieved(new RecievedPacketData(result, client));
                }
            }
        }

        internal static async Task sendPacket(Client client, Packet packet) { 
            TcpClient tcpClient = client.TcpClient;
            NetworkStream stream = tcpClient.GetStream();
            if (!client.ct.IsCancellationRequested) await stream.WriteAsync(Encoding.ASCII.GetBytes(convertPacketToJson(packet)), client.ct);
        }

        internal static string convertPacketToJson(Packet packet) {
            return Newtonsoft.Json.JsonConvert.SerializeObject(packet);
        }

        internal static void handleUnoNetPacket(RecievedPacketData client)
        {
            Console.WriteLine("Recieved UnoNet packet");
            switch ((PacketEvents)client.packet.get("Event")) { 
                case PacketEvents.Disconnect:
                    Server.KickClient(client.client.ID, DisconnectReason.Disconnected);
                    
                    break;
            }

        }
    }
}
