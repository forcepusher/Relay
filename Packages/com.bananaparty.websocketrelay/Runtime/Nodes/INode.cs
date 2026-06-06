namespace BananaParty.WebSocketRelay
{
    public interface INode
    {
        string Name { get; }
        void WriteStateToJson(JsonStateGraph jsonStateGraph);
        // void WriteBinaryState(BinaryStateGraph binaryStateGraph);
    }
}
