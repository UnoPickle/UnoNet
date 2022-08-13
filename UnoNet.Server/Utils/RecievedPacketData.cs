using System.Text;

namespace UnoNet.Server.Utils
{
    public class RecievedPacketData {
        public string data { get; }
        public Client client { get; }

        public RecievedPacketData(string data, Client client) { 
            this.data = data;
            this.client = client;
        }

        public RecievedPacketData(byte[] data, Client client)
        {
            this.data = Encoding.ASCII.GetString(data);
            this.client = client;
        }
    }
}
