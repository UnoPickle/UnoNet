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

        /// <summary>
        /// ID of the machine in the server
        /// </summary>
        public static int ID { get; internal set; }
        /// <summary>
        /// Boolean value based on whether the machine is connected to a server or not
        /// </summary>
        public static bool IsConnected { get; private set; } = false;

        /// <summary>
        /// Connect to a server
        /// </summary>
        /// <param name="address">Address of the server</param>
        /// <param name="port">Port of the server</param>
        /// <returns>A boolean based on whether was successful or not</returns>
        public static bool Connect(string address, int port) {
            if (!IsConnected) {
                IP ip = new IP(address, port);
                client = new TcpClient();
                try
                {
                    client.Connect(ip.getIP(), ip.Port);
                    var task = PacketManager.listenForPackets();
                    IsConnected = true;
                    return true;
                }
                catch /*(Exception e) */
                {
                    IsConnected = false;
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Connect to a server
        /// </summary>
        /// <param name="ServerIP">The server ip, containing the port (Address:Port)</param>
        /// <returns>A boolean based on whether was successful or not</returns>
        public static bool Connect(string ServerIP)
        {
            IP ip = IP.splicePort(ServerIP);
            if (ip.Port == 0) ip.Port = Defaults.Port;
            return Connect(ip.Address, ip.Port);
        }

        /// <summary>
        /// Disconnect from connected server
        /// </summary>
        public static void Disconnect() {
            if (IsConnected) {
                sendPacket(Packets.disconnectPacket(ID ,DisconnectReason.Disconnected));
                client.Close();
                client = null;
                ID = 0;
                cts.Cancel();
                IsConnected = false;
            }  
        }

        /// <summary>
        /// Send a packet to the server
        /// </summary>
        /// <param name="packet">Packet to send</param>
        public static void sendPacket(Packet packet) {
            PacketManager.sendPacket(packet, client);
        }

        /// <summary>
        /// Send a packet to all the connected clients
        /// </summary>
        /// <param name="packet">Packet to send</param>
        public static void sendToAll(Packet packet) {
            PacketManager.sendPacket(Packets.clientToAll(packet), client);
        }

        /// <summary>
        /// Get the IDs of all the connected clients
        /// </summary>
        /// <returns>A List<int> containing all the IDs</returns>
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
        public static EventHandler<NewClientArgs> OnClientConnecting;

        public static EventHandler<ClientDisconnectingArgs> OnClientDisconnect;

        internal static void InvokeOnPacketRecieved(Packet packet) {
            if(!packet.isUnoNetPacket) OnPacketRecieved?.Invoke(null, packet);
            if (packet.isUnoNetPacket) PacketManager.handleUnoNetPackets(packet);
        }

        internal static void InvokeClientConnecting(NewClientArgs args) {
            OnClientConnecting?.Invoke(null, args);
        }

        internal static void InvokeOnClientDisconnect(ClientDisconnectingArgs args) {
            OnClientDisconnect?.Invoke(null, args);
        }
    }
}
