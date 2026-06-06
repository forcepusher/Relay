namespace BananaParty.WebSocketRelay
{
    public interface IState<T>
    {
        void Write(IStateGraph<T> stateGraph);
        void Read(IStateGraph<T> stateGraph);
    }
}
