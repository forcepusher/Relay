namespace BananaParty.WebSocketRelay
{
    public interface INode
    {
        string Name { get; }
        void WriteStateToJson(JsonWriteStateGraph jsonStateGraph);
        // void WriteBinaryState(BinaryStateGraph binaryStateGraph);
    }
}
