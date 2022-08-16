using System;
using System.Collections.Generic;
using System.Text;

namespace UnoNet.Core
{
    /// <summary>
    /// Data transfer unit
    /// </summary>
    public class Packet
    {
        /// <summary>
        /// String - name, Object - Variable (needs to be casted for usage)
        /// </summary>
        public Dictionary<string, object> data { get; }

        public bool isUnoNetPacket { get; }

        /// <summary>
        /// Get a variable by name
        /// </summary>
        /// <param name="name">Name of the variable</param>
        /// <returns>Returns an Object (needs to be casted for usage)</returns>
        public object get(string name) {
            object value;
            data.TryGetValue(name, out value);
            return value;
        }
        /// <summary>
        /// Data transfer unit
        /// </summary>
        /// <param name="data">String - Name, Object - Variable</param>
        public Packet(Dictionary<string, Object> data) { 
            this.data = data;
            if (data.ContainsKey("UnoNet")) isUnoNetPacket = true;
        }

        /// <summary>
        /// Get a string that represents this class
        /// </summary>
        /// <returns>Data variable in a string format</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, object> kv in data) {
                sb.AppendLine(kv.Key + " - " + kv.Value);
            }
            return sb.ToString();
        }

    }
}
