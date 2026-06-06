namespace BananaParty.WebSocketRelay
{
    public interface INode
    {
        string Name { get; }
        void WriteState<T>(IStateGraph<T> stateGraph);
    }
}
