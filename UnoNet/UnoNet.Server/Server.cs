using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

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
                catch(Exception e)
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
            int port = 6666; //replace with UnoNet core defaults
            return init(port);
        }

        
        public static List<Client> getAllClients() {
            return Utils.ClientManager.clients;
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
            OnPacketRecieved?.Invoke(null, data);
        }

        internal static void InvokeOnClientConnects(Utils.ClientConnectionArgs args) { 
            OnClientConnects?.Invoke(null, args);    
        }

    }
}
