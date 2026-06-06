namespace BananaParty.WebSocketRelay
{
    public interface IObjectNode : INode
    {
        INode[] GetNodes();
    }
}
