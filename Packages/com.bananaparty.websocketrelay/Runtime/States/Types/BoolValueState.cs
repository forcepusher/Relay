namespace BananaParty.WebSocketRelay
{
    public class BoolValueState : IState
    {
        public string Name { get; private set; }
        public bool Value { get; set; }

        public BoolValueState(string name, bool initialValue)
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
            Value = readGraph.ReadBool(Name);
        }
    }
}
