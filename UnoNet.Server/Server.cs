﻿using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using UnoNet.Core;

namespace UnoNet.Server
{
    public class TestPacketClassData{
        public string Data { get; set; }

    }

    /// <summary>
    /// This class manages all the server main functions
    /// </summary>
    public static class Server
    {
        internal static TcpListener listener;
        internal static CancellationToken serverCT = new CancellationToken();
        

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
                    var task = Utils.ClientManager.listenForClientConnections(listener, serverCT);
                    return true;
                }
                catch//(Exception e)
                {
                    //Console.WriteLine(e);
                }
            }
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

        public static void close() { 
            //Implement dis
        }

        public static void sendPacket(int ID, Packet packet) {
            var task = Utils.PacketManager.sendPacket(Utils.ClientManager.GetClient(ID), packet);
        }

        public static void sendToAll() { 
            
        }


        /// <summary>
        /// Get all the connected clients
        /// </summary>
        /// <returns>Returns List<Client> with all the connected clients</returns>
        public static List<Client> getAllClients() {
            return Utils.ClientManager.clients;
        }

        /// <summary>
        /// Remove a connected client
        /// </summary>
        /// <param name="ID">ID of the client</param>
        public static void KickClient(int ID) {
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

        internal static void InvokeOnPacketRecieved(Utils.RecievedPacketData data)
        {
            if(data.packet != null) OnPacketRecieved?.Invoke(null, data);
        }

        internal static void InvokeOnClientConnects(Utils.ClientConnectionArgs args) { 
            OnClientConnects?.Invoke(null, args);    
        }

    }
}