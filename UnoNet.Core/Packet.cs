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

        /// <summary>
        /// Get a variable by name
        /// </summary>
        /// <param name="name">Name of the variable</param>
        /// <returns>Returns an Object (needs to be casted for usage)</returns>
        public object get(string name) {
            Object value;
            data.TryGetValue(name, out value);
            return value;
        }
        /// <summary>
        /// Data transfer unit
        /// </summary>
        /// <param name="data">String - Name, Object - Variable</param>
        public Packet(Dictionary<string, Object> data) { 
            this.data = data;
        }

    }
}
