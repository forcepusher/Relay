namespace BananaParty.WebSocketRelay
{
    public class StringObjectNode : IValueNode<string>
    {
        public string Name { get; private set; }
        public string Value { get; set; }

        public StringObjectNode(string name, string initialValue)
        {
            Name = name;
            Value = initialValue;
        }
    }
}
