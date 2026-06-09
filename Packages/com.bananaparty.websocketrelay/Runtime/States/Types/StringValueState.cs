namespace BananaParty.WebSocketRelay
{
    public class StringValueState : IState
    {
        public string StateName { get; private set; }
        public string Value { get; set; }

        public StringValueState(string name, string initialValue)
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
            Value = readGraph.ReadStringEntry(StateName);
        }
    }
}
