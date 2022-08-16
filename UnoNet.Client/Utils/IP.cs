using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace UnoNet.Client.Utils
{
    /// <summary>
    /// IP class
    /// </summary>
    public class IP
    {
        public string Address { set; get; }
        public int Port { set; get; }

        internal IPAddress getIP()
        {
            return IPAddress.Parse(Address);
        }

        public IP(string Address)
        {
            this.Address = Address;
        }

        public IP(string address, int port)
        {
            this.Address = address;
            Port = port;
        }

        internal static IP splicePort(string IP)
        {
            if (IP.Contains(":"))
            {
                string[] address = IP.Split(':');
                IP _ip = new IP(address[0].Replace(':', ' '));
                _ip.Port = int.Parse(address[1]);
                return _ip;
            }
            return new IP(IP);
        }
    }
}
