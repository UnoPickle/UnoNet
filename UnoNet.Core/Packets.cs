using System;
using System.Collections.Generic;
using System.Text;

namespace UnoNet.Core
{
    public static class Packets
    {
        /// <summary>
        /// Internal function. Use provided function (UnoNet.Client.Client.Disconnect)
        /// </summary>
        public static Packet disconnectPacket(DisconnectReason reason){
            return new Packet(new Dictionary<string, object>() { { "UnoNet", true }, { "Event", PacketEvents.Disconnect}, { "Reason", reason} });
        }

        /// <summary>
        /// Internal function
        /// </summary>
        public static Packet regID(int ID) {
            return new Packet(new Dictionary<string, object>() { {"UnoNet", true }, { "Event", PacketEvents.RegID}, { "ID", ID} });
        }

        /// <summary>
        /// Internal function. Use provided function (UnoNet.Client.Client.SendToAll/ UnoNet.Server.Server.SendToAll)
        /// </summary>
        public static Packet clientToAll(Packet packet) {
            Packet _packet = packet;
            _packet.data.Add("UnoNet", true);
            _packet.data.Add("Event", PacketEvents.ClientToAll);
            return _packet;
        }
        
        /// <summary>
        /// Internal function
        /// </summary>
        public static Packet newClient(int ID) {
            return new Packet(new Dictionary<string, object>() { { "UnoNet", true}, { "Event", PacketEvents.NewClient}, { "ID", ID} });
        }

        /// <summary>
        /// Internal function. Use provided function (UnoNet.Client.Client.GetAllIDS)
        /// </summary>
        public static Packet getAllClients() {
            return new Packet(new Dictionary<string, object>() { { "UnoNet", true}, { "Event", PacketEvents.GetAllIDS} });
        }
    }
}