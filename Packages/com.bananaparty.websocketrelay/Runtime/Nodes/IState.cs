namespace BananaParty.WebSocketRelay
{
    public interface IState<T>
    {
        void Write(IStateGraph<T> dataGraph);
        void Read(IStateGraph<T> dataGraph);
    }
}
