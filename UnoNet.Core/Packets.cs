using System;
using System.Collections.Generic;
using System.Text;

namespace UnoNet.Core
{
    public static class Packets
    {
        public static Packet disconnectPacket(DisconnectReason reason){
            return new Packet(new Dictionary<string, object>() { { "UnoNet", true }, { "Event", PacketEvents.Disconnect }, { "Reason", reason } });
        }
    }
}