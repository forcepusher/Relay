namespace BananaParty.WebSocketRelay
{
    public class BoolObjectNode : IValueNode<bool>
    {
        public string Name { get; private set; }
        public bool Value { get; set; }

        public BoolObjectNode(string name, bool initialValue)
        {
            Name = name;
            Value = initialValue;
        }
    }
}
