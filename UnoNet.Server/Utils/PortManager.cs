using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace UnoNet.Server.Utils
{
    internal class PortManager
    {
        internal static bool checkPortAvailablilty(int port) {
            bool isAvailable = true;

            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            foreach (TcpConnectionInformation tcpi in tcpConnInfoArray)
            {
                if (tcpi.LocalEndPoint.Port == port)
                {
                    isAvailable = false;
                    break;
                }
            }
            return isAvailable;
        }
    }
}
