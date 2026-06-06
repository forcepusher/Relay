namespace BananaParty.WebSocketRelay
{
    public interface IState<T>
    {
        void Write(IDataGraph<T> dataGraph);
        void Read(IDataGraph<T> dataGraph);
    }
}
