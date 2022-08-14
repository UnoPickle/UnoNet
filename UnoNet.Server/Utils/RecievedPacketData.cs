using System;
using System.Text;
using UnoNet.Core;

namespace UnoNet.Server.Utils
{
    public class RecievedPacketData {
        public Packet packet { get; }
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
