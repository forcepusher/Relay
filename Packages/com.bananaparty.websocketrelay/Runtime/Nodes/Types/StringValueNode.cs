namespace BananaParty.WebSocketRelay
{
    public class StringValueNode
    {
        public string Name { get; private set; }
        public string Value { get; set; }

        public StringValueNode(string name, string initialValue)
        {
            Name = name;
            Value = initialValue;
        }

        public void WriteStateToJson(JsonWriteGraph stateGraph)
        {
            stateGraph.WriteEntry(Name, Value, true);
        }

        public void ReadStateFromJson(JsonReadGraph stateGraph)
        {
            Value = stateGraph.ReadEntry(Name);
        }
    }
}
