namespace BananaParty.WebSocketRelay
{
    public class IntegerValueNode : IValueNode<int>
    {
        public int Value { get; set; }
        public string Name { get; private set; }

        public IntegerValueNode(string name, int initialValue)
        {
            Name = name;
            Value = initialValue;
        }
    }
}
