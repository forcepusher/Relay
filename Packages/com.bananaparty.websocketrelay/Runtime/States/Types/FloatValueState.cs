namespace BananaParty.WebSocketRelay
{
    public class FloatValueState : IState
    {
        public string StateName { get; private set; }
        public float Value { get; set; }

        public FloatValueState(string name, float initialValue)
        {
            StateName = name;
            Value = initialValue;
        }

        public void WriteState(IStateOutput writeGraph)
        {
            writeGraph.WriteEntry(StateName, Value);
        }

        public void ReadState(IStateInput readGraph)
        {
            Value = readGraph.ReadFloat(StateName);
        }
    }
}
