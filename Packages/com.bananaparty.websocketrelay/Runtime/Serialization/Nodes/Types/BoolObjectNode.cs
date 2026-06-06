namespace BananaParty.WebSocketRelay
{
    public class BoolObjectNode : IValueNode
    {
        public string Name { get; private set; }
        public bool Value;

        public BoolObjectNode(string name, bool initialValue)
        {
            Value = initialValue;
            Name = name;
        }
    }
}
