namespace BananaParty.WebSocketRelay
{
    public interface IStateGraphObject
    {
        void BuildStateGraph(IStateNode parent);
    }
}
