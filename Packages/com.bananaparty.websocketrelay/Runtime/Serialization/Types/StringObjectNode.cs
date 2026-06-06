namespace BananaParty.WebSocketRelay
{
    public class StringObjectNode : IObjectNode
    {
        public string Value;
        public readonly string Name;

        public StringObjectNode(string name, string initialValue)
        {
            Value = initialValue;
            Name = name;
        }
    }
}
