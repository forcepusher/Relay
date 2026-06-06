namespace BananaParty.WebSocketRelay
{
    public class IntegerObjectNode : IValueNode<int>
    {
        public int Value { get; set; }
        public string Name { get; private set; }

        public IntegerObjectNode(string name, int initialValue)
        {
            Name = name;
            Value = initialValue;
        }
    }
}
