namespace BananaParty.WebSocketRelay
{
    public interface ISerializableState
    {
        void BuildStateGraph(StateGraphNode parent);
        // void Serialize(StateNode stateGraph);
        // void Deserialize(StateNode stateGraph);
    }
}
