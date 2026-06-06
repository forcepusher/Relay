namespace BananaParty.WebSocketRelay
{
    public class ByteObjectNode : IObjectNode
    {
        public byte Value;
        public readonly string Name;

        public ByteObjectNode(string name, byte initialValue)
        {
            Value = initialValue;
            Name = name;
        }
    }
}
