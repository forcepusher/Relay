namespace BananaParty.WebSocketRelay
{
    public class BoolObjectNode : IObjectNode
    {
        public bool Value;
        public readonly string Name;

        public BoolObjectNode(string name, bool initialValue)
        {
            Value = initialValue;
            Name = name;
        }
    }
}
