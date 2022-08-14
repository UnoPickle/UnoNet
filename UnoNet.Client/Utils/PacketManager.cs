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
