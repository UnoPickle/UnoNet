using System.Text;

namespace UnoNet.Server.Utils
{
    public class RecievedPacketData {
        public string data { get; }
        public RecievedPacketData(string data) { 
            this.data = data;
        }

        public RecievedPacketData(byte[] data)
        {
            this.data = Encoding.ASCII.GetString(data);
        }
    }
}
