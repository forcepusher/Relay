namespace BananaParty.WebSocketRelay
{
    public interface IState<T>
    {
        void WriteJsonState(JsonStateGraph stateGraph);
        void ReadJsonState(JsonStateGraph stateGraph);
    }
}
