namespace BananaParty.WebSocketRelay
{
    public interface ISerializableState
    {
        void BuildStateGraph(StateGraphNode parent);
    }
}
