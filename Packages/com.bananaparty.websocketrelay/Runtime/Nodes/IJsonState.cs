namespace BananaParty.WebSocketRelay
{
    public interface IJsonState : IState
    {
        void WriteToJson(JsonWriteGraph jsonWriteGraph);
        void ReadFromJson(JsonReadGraph jsonReadGraph);
    }
}
