using System;
using System.Collections.Generic;
using System.Text;
using UnoNet.Core;

namespace UnoNet.Client.Utils
{
    /// <summary>
    /// Information that gets passed when a new client connects
    /// </summary>
    public class NewClientArgs
    {
        /// <summary>
        /// ID of the connecting client
        /// </summary>
        public int ID { get; }

        public NewClientArgs(int ID)
        {
            this.ID = ID;
        }
    }
}
