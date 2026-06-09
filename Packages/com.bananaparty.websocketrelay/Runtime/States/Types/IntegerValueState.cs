namespace BananaParty.WebSocketRelay
{
    public class IntegerValueState : IState
    {
        public int Value { get; set; }
        public string Name { get; private set; }

        public IntegerValueState(string name, int initialValue)
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
            Value = readGraph.ReadInt(Name);
        }
    }
}
