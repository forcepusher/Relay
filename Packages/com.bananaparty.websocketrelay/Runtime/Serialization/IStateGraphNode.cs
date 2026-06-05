namespace BananaParty.WebSocketRelay
{
    public interface IStateGraphNode : IState
    {
        void AddChildStateGraphNode(ObjectGraphNode stateGraphNode);

        void AddState(IState state);
    }
}
