namespace BananaParty.WebSocketRelay
{
    public class FloatValueNode
    {
        public string Name { get; private set; }
        public float Value { get; set; }

        public FloatValueNode(string name, float initialValue)
        {
            Name = name;
            Value = initialValue;
        }

        public void WriteStateToJson(JsonWriteStateGraph stateGraph)
        {
            stateGraph.WriteEntry(Name, Value.ToString(), false);
        }

        public void ReadStateFromJson(JsonReadStateGraph stateGraph)
        {
            string val = stateGraph.ReadEntry(Name);
            if (val != null && float.TryParse(val, out float result))
                Value = result;
        }
    }
}
