namespace BananaParty.WebSocketRelay
{
    public interface IState
    {
        string StateName { get; }
        void WriteState(IStateOutput stateOutput);
        void ReadState(IStateInput stateInput);
        void CopyFrom(IState other);
    }
}
