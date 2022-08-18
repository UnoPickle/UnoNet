using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using UnoNet.Core;
using UnoNet.Server.Utils;

namespace UnoNet.Server
{
    /// <summary>
    /// This class manages all the server main functions
    /// </summary>
    public static class Server
    {
        internal static TcpListener listener;
        internal static CancellationTokenSource serverCts = new CancellationTokenSource();

        /// <summary>
        /// Boolean value based of if the server is running or not
        /// </summary>
        public static bool IsRunning { get; private set; } = false;

        #region Server Core Functions
        /// <summary>
        /// Initializes server with the specified port
        /// </summary>
        /// <returns>Returns a bool value based on if the listener started or not</returns>
        /// <param name="port">Specify the port on which the server will listen</param>
        public static bool init(int port) {
            listener = new TcpListener(System.Net.IPAddress.Any, port);
            if (Utils.PortManager.checkPortAvailablilty(port)) {
                try
                {
                    listener.Start();
                    var task = Utils.ClientManager.listenForClientConnections(listener, serverCts.Token);
                    IsRunning = true;
                    return true;
                }
                catch//(Exception e)
                {
                    //Console.WriteLine(e);
                    IsRunning = false;
                }
            }
            IsRunning = false;
            return false;
        }

        /// <summary>
        /// Initializes server with the default port
        /// </summary>
        /// <returns>Returns a bool value based on if the listener started or not</returns>
        public static bool init()
        {
            return init(Core.Defaults.Port);
        }


        /// <summary>
        /// Closes the server
        /// </summary>
        public static void close() {
            if (IsRunning) {
                sendToAll(Packets.serverClosing());
                listener.Stop();
                IsRunning = false;
            }
            
        }

        #endregion

        #region Packet Sending Functions

        /// <summary>
        /// Send a packet to a client
        /// </summary>
        /// <param name="ID">ID of the client</param>
        /// <param name="packet">Data to send</param>
        public static void sendPacket(int ID, Packet packet) {
            var task = Utils.PacketManager.sendPacket(Utils.ClientManager.GetClient(ID), packet);
        }
        /// <summary>
        /// Send a packet to all the connected clients
        /// </summary>
        /// <param name="packet">Data to send</param>
        public static void sendToAll(Packet packet) {
            var task = Utils.PacketManager.SendToAll(packet);
        }

        #endregion

        /// <summary>
        /// Get all the connected clients
        /// </summary>
        /// <returns>Returns a List with all the connected clients</returns>
        public static List<Client> getAllClients() {
            return Utils.ClientManager.clients;
        }

        /// <summary>
        /// Remove a connected client
        /// </summary>
        /// <param name="ID">ID of the client</param>
        public static void KickClient(int ID, DisconnectReason reason) {
            InvokeOnClientDisconnects(new Utils.ClientDisconnectEventArgs(Utils.ClientManager.GetClient(ID), reason));
            Utils.ClientManager.removeClient(ID);

        }

        /// <summary>
        /// Check if a client exists
        /// </summary>
        /// <param name="ID">ID of the client</param>
        /// <returns>Returns a bool value based on if the client exists or not</returns>
        public static bool checkClientExsitance(int ID) {
            return !Utils.ClientManager.checkIDAvailiblity(ID);
        }


        /// <summary>
        /// Gets called every time the listener recieves a packet
        /// </summary>
        public static EventHandler<Utils.RecievedPacketData> OnPacketRecieved;
        /// <summary>
        /// Gets called every time a client connects to the server
        /// </summary>
        public static EventHandler<Utils.ClientConnectionArgs> OnClientConnects;
        /// <summary>
        /// Gets called every time a client disconnects from the server
        /// </summary>
        public static EventHandler<Utils.ClientDisconnectEventArgs> OnClientDisconnects;

        internal static void InvokeOnPacketRecieved(Utils.RecievedPacketData data)
        {
            if (data.packet != null) {
                if (!data.packet.isUnoNetPacket) OnPacketRecieved?.Invoke(null, data);
                if (data.packet.isUnoNetPacket) Utils.PacketManager.handleUnoNetPacket(data);
            }
            
        }

        internal static void InvokeOnClientConnects(Utils.ClientConnectionArgs args) {
            Server.OnClientConnects?.Invoke(null, args);
        }

        internal static void InvokeOnClientDisconnects(Utils.ClientDisconnectEventArgs args) {
            OnClientDisconnects?.Invoke(null, args);
            sendToAll(Packets.disconnectPacket(args.client.ID,args.reason));
        }

    }
}
