namespace BananaParty.WebSocketRelay
{
    public interface INode
    {
        string Name { get; }
        void WriteStateToJson(JsonWriteStateGraph jsonStateGraph);
        void ReadStateFromJson(JsonReadStateGraph jsonReadStateGraph);
        // void WriteBinaryState(BinaryStateGraph binaryStateGraph);
    }
}
