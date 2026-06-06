namespace BananaParty.WebSocketRelay
{
    public class StringObjectNode : IValueNode
    {
        public string Name { get; private set; }
        public string Value;

        public StringObjectNode(string name, string initialValue)
        {
            Value = initialValue;
            Name = name;
        }
    }
}
