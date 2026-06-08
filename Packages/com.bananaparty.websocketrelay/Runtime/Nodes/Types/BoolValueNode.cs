namespace BananaParty.WebSocketRelay
{
    public class BoolValueNode
    {
        public string Name { get; private set; }
        public bool Value { get; set; }

        public BoolValueNode(string name, bool initialValue)
        {
            Name = name;
            Value = initialValue;
        }

        public void WriteStateToJson(JsonWriteStateGraph stateGraph)
        {
            stateGraph.WriteEntry(Name, Value.ToString().ToLowerInvariant(), false);
        }

        public void ReadStateFromJson(JsonReadStateGraph stateGraph)
        {
            string val = stateGraph.ReadEntry(Name);
            if (val != null && bool.TryParse(val, out bool result))
                Value = result;
        }
    }
}
