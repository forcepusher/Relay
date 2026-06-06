namespace BananaParty.WebSocketRelay
{
    public interface IJsonState
    {
        void WriteStateToJson(JsonStateGraph stateGraph);
        void ReadStateFromJson(JsonStateGraph stateGraph);
    }
}
