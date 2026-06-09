namespace BananaParty.WebSocketRelay
{
    public class StringValueState : IState
    {
        public string Name { get; private set; }
        public string Value { get; set; }

        public StringValueState(string name, string initialValue)
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
            Value = readGraph.ReadStringEntry(Name);
        }
    }
}
