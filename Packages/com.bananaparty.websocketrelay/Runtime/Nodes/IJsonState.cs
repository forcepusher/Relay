namespace BananaParty.WebSocketRelay
{
    public interface IJsonState
    {
        void WriteStateToJson(JsonWriteStateGraph stateGraph);
        void ReadStateFromJson(JsonWriteStateGraph stateGraph);
    }
}
