using System;
using System.Collections.Generic;
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
        internal static List<int> recievedClientIDs = new List<int>();

        public static int ID;
        public static bool IsConnected { get; private set; } = false;

        public static bool Connect(string address, int port) {
            IP ip = new IP(address, port);
            client = new TcpClient();
            try
            {
                client.Connect(ip.getIP(), ip.Port);
                var task = PacketManager.listenForPackets();
                IsConnected = true;
                return true;
            }
            catch /*(Exception e) */{
                IsConnected = false;
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
            cts.Cancel();
            IsConnected = false;
        }

        public static void sendPacket(Packet packet) {
            PacketManager.sendPacket(packet, client);
        }

        public static void sendToAll(Packet packet) {
            PacketManager.sendPacket(Packets.clientToAll(packet), client);
        }

        public static List<int> getAllIDS() {
            sendPacket(Packets.getAllClients());
            return recievedClientIDs;
        }

        /// <summary>
        /// Gets called every time the client recieves a packet 
        /// </summary>
        public static EventHandler<Packet> OnPacketRecieved;

        /// <summary>
        /// Gets called every time a client joins the server 
        /// </summary>
        public static EventHandler<NewClientArgs> OnNewClient;

        internal static void InvokeOnPacketRecieved(Packet packet) {
            if(!packet.isUnoNetPacket) OnPacketRecieved?.Invoke(null, packet);
            if (packet.isUnoNetPacket) PacketManager.handleUnoNetPackets(packet);
        }

        internal static void InvokeOnNewClient(NewClientArgs args) {
            OnNewClient?.Invoke(null, args);
        }
    }
}
