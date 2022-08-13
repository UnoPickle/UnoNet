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
        }

        public RecievedPacketData(byte[] data, Client client)
        {
            packet = Newtonsoft.Json.JsonConvert.DeserializeObject<Packet>(Encoding.ASCII.GetString(data));
            this.client = client;
        }
    }
}
