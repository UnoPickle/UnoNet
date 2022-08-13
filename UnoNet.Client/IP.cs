using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace UnoNet.Client
{
    public class IP
    {
        public string Address { set; get; }
        public int Port { set; get; }
        public IPAddress getIP() {
            return IPAddress.Parse(Address);
        }

        public IP(string Address) {
            this.Address = Address;
        }

        public static IP splicePort(string IP)
        {
            if (IP.Contains(":")) {
                string[] address = IP.Split(':');
                IP _ip = new IP(address[0].Replace(':',' '));
                _ip.Port = int.Parse(address[1]);
                return _ip;
            }
            return new UnoNet.Client.IP(IP);
        }
    }
}
