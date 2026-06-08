namespace BananaParty.WebSocketRelay
{
    public interface IJsonState : IState
    {
        void WriteStateToJson(JsonWriteGraph jsonStateGraph);
        void ReadStateFromJson(JsonReadGraph jsonReadStateGraph);
    }
}
