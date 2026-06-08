namespace BananaParty.WebSocketRelay
{
    public class IntegerValueNode : IBinaryState
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
            stateGraph.WriteEntry(Name, Value);
        }

        public void ReadStateFromJson(JsonReadGraph stateGraph)
        {
            Value = stateGraph.ReadIntEntry(Name);
        }

        public void WriteToBinary(BinaryWriteGraph stateGraph)
        {
            stateGraph.WriteEntry(Name, Value);
        }

        public void ReadFromBinary(BinaryReadGraph stateGraph)
        {
            Value = stateGraph.ReadIntEntry(Name);
        }
    }
}
