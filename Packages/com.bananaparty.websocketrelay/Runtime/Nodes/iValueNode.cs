namespace BananaParty.WebSocketRelay
{
    public interface IValueNode<T> : INode
    {
        T Value { get; set; }
    }
}
