namespace BananaParty.WebSocketRelay
{
    public interface IStateNode
    {
        string Name { get; }
        void Write(IWriteGraph writeGraph);
        void Read(IReadGraph readGraph);
    }
}
