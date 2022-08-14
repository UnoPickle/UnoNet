using System;
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
        public static bool Connect(string ServerIP) {
            IP ip = IP.splicePort(ServerIP);
            if (ip.Port == 0 || ip.Port == null) ip.Port = Defaults.Port;
            return Connect(ip.Address, ip.Port);
        }

        public static bool Connect(string address, int port) {
            IP ip = new IP(address, port);
            client = new TcpClient();
            try
            {
                client.Connect(ip.getIP(), ip.Port);
                return true;
            }
            catch /*(Exception e) */{
                return false;
            }
            
        }

        public static void Disconnect() {
            sendPacket(Packets.disconnectPacket(DisconnectReason.Disconnected));
            client.Close();
            client = null;
        }

        public static void sendPacket(Packet packet) {
            PacketManager.sendPacket(packet, client);
        }

        public static EventHandler<Packet> OnPacketRecieved;

        internal static void InvokeOnPacketRecieved(Packet packet) {
            OnPacketRecieved?.Invoke(null, packet);
        }
    }
}
