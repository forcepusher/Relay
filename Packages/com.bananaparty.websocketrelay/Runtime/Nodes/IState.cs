namespace BananaParty.WebSocketRelay
{
    public interface IState<T>
    {
        void WriteState(IStateGraph<T> stateGraph);
        void ReadState(IStateGraph<T> stateGraph);
    }
}
