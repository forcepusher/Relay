namespace BananaParty.WebSocketRelay
{
    public interface ISerializableState
    {
        void BuildStateGraph(IStateGraphNode parent);
    }
}
