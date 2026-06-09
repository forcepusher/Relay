namespace BananaParty.WebSocketRelay
{
    public interface IState
    {
        string Name { get; }
        void Write(IStateOutput writeGraph);
        void Read(IStateInput readGraph);
    }
}
