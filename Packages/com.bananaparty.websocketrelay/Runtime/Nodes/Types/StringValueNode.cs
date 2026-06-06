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

        public void WriteStateToJson(JsonWriteStateGraph stateGraph)
        {
            stateGraph.WriteEntry(Name, Value, true);
        }
    }
}
