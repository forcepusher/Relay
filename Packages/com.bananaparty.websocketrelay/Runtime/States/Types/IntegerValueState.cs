namespace BananaParty.WebSocketRelay
{
    public class IntegerValueState : IState
    {
        public int Value { get; set; }
        public string StateName { get; private set; }

        public IntegerValueState(string name, int initialValue)
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
            Value = readGraph.ReadInt(StateName);
        }
    }
}
