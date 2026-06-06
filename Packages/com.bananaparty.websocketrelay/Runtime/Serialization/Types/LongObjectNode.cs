namespace BananaParty.WebSocketRelay
{
    public class LongObjectNode : IObjectNode
    {
        public long Value;
        public readonly string Name;

        public LongObjectNode(string name, long initialValue)
        {
            Value = initialValue;
            Name = name;
        }
    }
}
