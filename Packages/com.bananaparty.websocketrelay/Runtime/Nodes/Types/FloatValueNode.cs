namespace BananaParty.WebSocketRelay
{
    public class FloatValueNode : IBinaryState
    {
        public string Name { get; private set; }
        public float Value { get; set; }

        public FloatValueNode(string name, float initialValue)
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
            Value = stateGraph.ReadFloatEntry(Name);
        }

        public void WriteToBinary(BinaryWriteGraph stateGraph)
        {
            stateGraph.WriteEntry(Name, Value);
        }

        public void ReadFromBinary(BinaryReadGraph stateGraph)
        {
            Value = stateGraph.ReadFloatEntry(Name);
        }
    }
}
