namespace BananaParty.WebSocketRelay
{
    public interface IState
    {
        string Name { get; }
        void Write(IStateOutput stateOutput);
        void Read(IStateInput stateInput);
    }
}
