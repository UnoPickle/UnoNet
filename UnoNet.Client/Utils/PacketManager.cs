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
        internal static async Task listenForPackets() {
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
            byte[] bytes = Encoding.ASCII.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(packet));
            client.GetStream().Write(bytes, 0, bytes.Length);
        }

        internal static async Task sendPacketAsync(Packet packet, TcpClient client, CancellationToken ct) {
            byte[] bytes = Encoding.ASCII.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(packet));
            await client.GetStream().WriteAsync(bytes,0,bytes.Length, ct);
        }

        internal static Packet convertJsonToPacket(string json) {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Packet>(json);
        }
        internal static Packet convertBytesToPacket(byte[] data) {
            return convertJsonToPacket(Encoding.ASCII.GetString(data));
        }

        internal static void handleUnoNetPackets(Packet packet) {
            switch (int.Parse(packet.get("Event").ToString())) {
                case (int)PacketEvents.RegID:
                    Client.ID = int.Parse(packet.get("ID").ToString());
                    //Console.WriteLine(Client.ID);
                    break;
                case (int)PacketEvents.NewClient:
                    Client.InvokeOnNewClient(new NewClientArgs(int.Parse(packet.get("ID").ToString())));
                    break;
            }
        }
    }
}
