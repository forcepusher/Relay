namespace BananaParty.WebSocketRelay
{
    public class IntegerObjectNode : IValueNode
    {
        public int Value;
        public string Name { get; private set; }

        public IntegerObjectNode(string name, int initialValue)
        {
            Value = initialValue;
            Name = name;
        }
    }
}
