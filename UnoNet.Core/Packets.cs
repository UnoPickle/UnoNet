using System;
using System.Collections.Generic;
using System.Text;

namespace UnoNet.Core
{
    public static class Packets
    {
        public static Packet disconnectPacket(DisconnectReason reason){
            return new Packet(new Dictionary<string, object>() { { "UnoNet", true }, { "Event", PacketEvents.Disconnect}, { "Reason", reason} });
        }

        public static Packet regID(int ID) {
            return new Packet(new Dictionary<string, object>() { {"UnoNet", true }, { "Event", PacketEvents.RegID}, { "ID", ID} });
        }

        public static Packet clientToAll(Packet packet) {
            Packet _packet = packet;
            _packet.data.Add("UnoNet", true);
            _packet.data.Add("Event", PacketEvents.ClientToAll);
            return _packet;
        }
        
        public static Packet newClient(int ID) {
            return new Packet(new Dictionary<string, object>() { { "UnoNet", true}, { "Event", PacketEvents.NewClient}, { "ID", ID} });
        }

        public static Packet getAllClients() {
            return new Packet(new Dictionary<string, object>() { { "UnoNet", true}, { "Event", PacketEvents.GetAllIDS} });
        }
    }
}