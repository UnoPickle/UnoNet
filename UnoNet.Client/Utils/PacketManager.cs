using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnoNet.Core;

namespace UnoNet.Client.Utils
{
    internal class PacketManager
    {
        internal async Task listenForPackets() {
            TcpClient client = Client.client;
            using (client) {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[client.ReceiveBufferSize];
                byte[] result;
                while (!Client.cts.Token.IsCancellationRequested) {
                    result = new byte[buffer.Length];
                    await stream.ReadAsync(result, 0, (int)buffer.Length);
                    if (result != null) Client.InvokeOnPacketRecieved(convertBytesToPacket(result));
                }
            }
        }

        internal static void sendPacket(Packet packet, TcpClient client) {
            client.GetStream().Write(Encoding.ASCII.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(packet)));
        }

        internal static async Task sendPacketAsync(Packet packet, TcpClient client, CancellationToken ct) {
            await client.GetStream().WriteAsync(Encoding.ASCII.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(packet)), ct);
        }

        internal static Packet convertJsonToPacket(string json) {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Packet>(json);
        }
        internal static Packet convertBytesToPacket(byte[] data) {
            return convertJsonToPacket(Encoding.ASCII.GetString(data));
        }
    }
}
