namespace BananaParty.WebSocketRelay
{
    public class IntegerValueNode
    {
        public int Value { get; set; }
        public string Name { get; private set; }

        public IntegerValueNode(string name, int initialValue)
        {
            Name = name;
            Value = initialValue;
        }

        public void WriteStateToJson(JsonWriteGraph stateGraph)
        {
            stateGraph.WriteEntry(Name, Value.ToString(), false);
        }

        public void ReadStateFromJson(JsonReadGraph stateGraph)
        {
            string val = stateGraph.ReadEntry(Name);
            if (val != null && int.TryParse(val, out int result))
                Value = result;
        }
    }
}
