using System;
using System.Text;
using UnoNet.Core;

namespace UnoNet.Server.Utils
{
    /// <summary>
    /// Data the gets passed when recieved a pacekt 
    /// </summary>
    public class RecievedPacketData {

        /// <summary>
        /// Recieved packet
        /// </summary>
        public Packet packet { get; }

        /// <summary>
        /// Sending client
        /// </summary>
        public Client client { get; }

        public RecievedPacketData(string data, Client client) { 
            packet = Newtonsoft.Json.JsonConvert.DeserializeObject<Packet>(data);
            this.client = client;
            //if (packet.data.ContainsKey("UnoNet")) isUnoNetPacket = true; 
        }

        public RecievedPacketData(byte[] data, Client client)
        {
            packet = Newtonsoft.Json.JsonConvert.DeserializeObject<Packet>(Encoding.ASCII.GetString(data));
            if (packet != null)
            {
                this.client = client;
                //if (packet.data.ContainsKey("UnoNet")) isUnoNetPacket = true;
            }
        }
    }
}
