namespace BananaParty.WebSocketRelay
{
    public interface INode
    {
        string Name { get; }
        void WriteJsonState(JsonStateGraph jsonStateGraph);
        // void WriteBinaryState(BinaryStateGraph binaryStateGraph);
    }
}
