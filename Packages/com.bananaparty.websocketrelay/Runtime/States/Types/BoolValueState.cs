namespace BananaParty.WebSocketRelay
{
    public class BoolValueState : IState
    {
        public string StateName { get; private set; }
        public bool Value { get; set; }

        public BoolValueState(string name, bool initialValue)
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
            Value = readGraph.ReadBool(StateName);
        }
    }
}
