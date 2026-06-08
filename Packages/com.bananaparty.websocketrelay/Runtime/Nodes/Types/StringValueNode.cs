namespace BananaParty.WebSocketRelay
{
    public class StringValueNode : IBinaryState
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
            stateGraph.WriteEntry(Name, Value);
        }

        public void ReadStateFromJson(JsonReadGraph stateGraph)
        {
            Value = stateGraph.ReadStringEntry(Name);
        }

        public void WriteToBinary(BinaryWriteGraph stateGraph)
        {
            stateGraph.WriteEntry(Name, Value);
        }

        public void ReadFromBinary(BinaryReadGraph stateGraph)
        {
            Value = stateGraph.ReadStringEntry(Name);
        }
    }
}
