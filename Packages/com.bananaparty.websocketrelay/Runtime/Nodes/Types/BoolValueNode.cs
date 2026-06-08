namespace BananaParty.WebSocketRelay
{
    public class BoolValueNode : IBinaryState
    {
        public string Name { get; private set; }
        public bool Value { get; set; }

        public BoolValueNode(string name, bool initialValue)
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
            Value = stateGraph.ReadBoolEntry(Name);
        }

        public void WriteToBinary(BinaryWriteGraph stateGraph)
        {
            stateGraph.WriteEntry(Name, Value);
        }

        public void ReadFromBinary(BinaryReadGraph stateGraph)
        {
            Value = stateGraph.ReadBoolEntry(Name);
        }
    }
}
