using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using UnoNet.Client.Utils;
using UnoNet.Core;

namespace UnoNet.Client
{
    public static class Client{
        internal static TcpClient client;
        internal static CancellationTokenSource cts = new CancellationTokenSource();
        public static int ID; 
        public static bool Connect(string address, int port) {
            IP ip = new IP(address, port);
            client = new TcpClient();
            try
            {
                client.Connect(ip.getIP(), ip.Port);
                var task = PacketManager.listenForPackets();
                return true;
            }
            catch /*(Exception e) */{
                return false;
            }
            
        }

        public static bool Connect(string ServerIP)
        {
            IP ip = IP.splicePort(ServerIP);
            if (ip.Port == 0) ip.Port = Defaults.Port;
            return Connect(ip.Address, ip.Port);
        }


        public static void Disconnect() {
            sendPacket(Packets.disconnectPacket(DisconnectReason.Disconnected));
            client.Close();
            client = null;
            ID = 0;
        }

        public static void sendPacket(Packet packet) {
            PacketManager.sendPacket(packet, client);
        }

        public static void sendToAll(Packet packet) {
            PacketManager.sendPacket(Packets.clientToAll(packet), client);
        }

        /// <summary>
        /// Gets called every time the client recieves a packet 
        /// </summary>
        public static EventHandler<Packet> OnPacketRecieved;

        internal static void InvokeOnPacketRecieved(Packet packet) {
            if(!packet.isUnoNetPacket) OnPacketRecieved?.Invoke(null, packet);
            if (packet.isUnoNetPacket) PacketManager.handleUnoNetPackets(packet);
        }
    }
}
