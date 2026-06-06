namespace BananaParty.WebSocketRelay
{
    public class IntegerObjectNode : IObjectNode
    {
        public int Value;
        public readonly string Name;

        public IntegerObjectNode(string name, int initialValue)
        {
            Value = initialValue;
            Name = name;
        }
    }
}
