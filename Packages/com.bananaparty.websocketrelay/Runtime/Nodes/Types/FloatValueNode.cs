namespace BananaParty.WebSocketRelay
{
    public class FloatValueNode : IStateNode
    {
        public string Name { get; private set; }
        public float Value { get; set; }

        public FloatValueNode(string name, float initialValue)
        {
            Name = name;
            Value = initialValue;
        }

        public void Write(IWriteGraph writeGraph)
        {
            writeGraph.WriteEntry(Name, Value);
        }

        public void Read(IReadGraph readGraph)
        {
            Value = readGraph.ReadFloatEntry(Name);
        }
    }
}
