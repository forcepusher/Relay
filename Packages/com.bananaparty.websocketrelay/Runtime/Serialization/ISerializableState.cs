namespace BananaParty.WebSocketRelay
{
    public interface ISerializableState
    {
        void BuildStateGraph(StateNode parent);
        // void Serialize(StateNode stateGraph);
        // void Deserialize(StateNode stateGraph);
    }
}
