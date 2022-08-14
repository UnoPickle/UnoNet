using System;
using System.Collections.Generic;
using System.Text;

namespace UnoNet.Client.Utils
{
    public class NewClientArgs
    {
        public int ID { get; }

        public NewClientArgs(int ID)
        {
            this.ID = ID;
        }
    }
}
