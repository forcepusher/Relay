namespace BananaParty.WebSocketRelay
{
    public interface IStateGraphNode : IState
    {
        void AddChildStateGraphNode(IStateGraphNode stateGraphNode);

        void AddState(IState state);
    }
}
