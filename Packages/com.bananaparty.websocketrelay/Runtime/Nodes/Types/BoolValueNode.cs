namespace BananaParty.WebSocketRelay
{
    public class BoolValueNode : IValueNode<bool>
    {
        public string Name { get; private set; }
        public bool Value { get; set; }

        public BoolValueNode(string name, bool initialValue)
        {
            Name = name;
            Value = initialValue;
        }
    }
}
