namespace BananaParty.WebSocketRelay
{
    public class FloatObjectNode : IValueNode<float>
    {
        public string Name { get; private set; }
        public float Value { get; set; }

        public FloatObjectNode(string name, float initialValue)
        {
            Name = name;
            Value = initialValue;
        }
    }
}
