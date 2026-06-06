namespace BananaParty.WebSocketRelay
{
    public class FloatValueNode : IValueNode<float>
    {
        public string Name { get; private set; }
        public float Value { get; set; }

        public FloatValueNode(string name, float initialValue)
        {
            Name = name;
            Value = initialValue;
        }

        public void WriteJsonState(JsonStateGraph stateGraph)
        {
            stateGraph.WriteEntry(Name, Value.ToString());
        }
    }
}
