namespace BananaParty.WebSocketRelay
{
    public class FloatValueState : IState
    {
        public string Name { get; private set; }
        public float Value { get; set; }

        public FloatValueState(string name, float initialValue)
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
