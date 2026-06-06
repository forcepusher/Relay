namespace BananaParty.WebSocketRelay
{
    public class StringValueNode : IValueNode<string>
    {
        public string Name { get; private set; }
        public string Value { get; set; }

        public StringValueNode(string name, string initialValue)
        {
            Name = name;
            Value = initialValue;
        }

        public void WriteJsonState(JsonStateGraph stateGraph)
        {
            stateGraph.WriteEntry(Name, Value);
        }
    }
}
