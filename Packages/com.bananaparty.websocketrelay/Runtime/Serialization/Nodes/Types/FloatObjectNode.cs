namespace BananaParty.WebSocketRelay
{
    public class FloatObjectNode : INode
    {
        public string Name { get; private set; }
        public float Value;

        public FloatObjectNode(string name, float initialValue)
        {
            Value = initialValue;
            Name = name;
        }
    }
}
