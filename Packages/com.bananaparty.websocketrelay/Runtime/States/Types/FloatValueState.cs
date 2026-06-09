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

        public void Write(IStateOutput writeGraph)
        {
            writeGraph.WriteEntry(Name, Value);
        }

        public void Read(IStateInput readGraph)
        {
            Value = readGraph.ReadFloat(Name);
        }
    }
}
