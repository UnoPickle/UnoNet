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
                while (!client.ct.IsCancellationRequested)
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
            byte[] bytes = Encoding.ASCII.GetBytes(convertPacketToJson(packet));
            if (!client.ct.IsCancellationRequested) await stream.WriteAsync(bytes, 0, bytes.Length, client.ct);
        }

        internal static async Task SendToAll(Packet packet) {
            foreach (Client client in ClientManager.clients) {
                await sendPacket(client, packet);
            }
        }

        internal static string convertPacketToJson(Packet packet) {
            return Newtonsoft.Json.JsonConvert.SerializeObject(packet);
        }

        internal static void handleUnoNetPacket(RecievedPacketData data)
        {
            //Console.WriteLine("Recieved UnoNet packet " + data.packet.ToString());
            switch (int.Parse(data.packet.get("Event").ToString())) { 
                case ((int)PacketEvents.Disconnect):    
                    Server.KickClient(data.client.ID, DisconnectReason.Disconnected);
                    break;

                case (int)PacketEvents.ClientToAll:
                    Dictionary<string, object> modifiedData = data.packet.data;
                    modifiedData.Remove("UnoNet");
                    modifiedData.Remove("Event");
                    Server.sendToAll(new Packet(modifiedData));
                    break;

                case (int)PacketEvents.GetAllIDS:
                    List<int> allIDS = new List<int>();
                    foreach (Client client in Server.getAllClients()) { 
                        allIDS.Add(client.ID);
                    }
                    Server.sendPacket(data.client.ID, new Packet(new Dictionary<string, object>() { { "UnoNet", true}, { "Event", PacketEvents.GetAllIDS}, { "IDs", allIDS} }));
                    break; 
                default:
                    //Console.WriteLine("Unknown Event");
                    break;
            }
            

        }
    }
}
