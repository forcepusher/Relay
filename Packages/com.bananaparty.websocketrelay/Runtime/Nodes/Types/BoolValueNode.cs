namespace BananaParty.WebSocketRelay
{
    public class BoolValueNode : IStateNode
    {
        public string Name { get; private set; }
        public bool Value { get; set; }

        public BoolValueNode(string name, bool initialValue)
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
            Value = readGraph.ReadBoolEntry(Name);
        }
    }
}
