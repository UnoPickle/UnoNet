namespace UnoNet.Core
{
    /// <summary>
    /// Internal UnoNet class
    /// </summary>
    public enum PacketEvents { 
        Disconnect,
        RegID,
        NewClient,
        ClientToAll,
        ClientToClient, //Implement later
        GetAllIDS,
        ServerClosing
    }
}
