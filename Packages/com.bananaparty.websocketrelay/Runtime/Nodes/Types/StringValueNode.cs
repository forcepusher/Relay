namespace BananaParty.WebSocketRelay
{
    public class StringValueNode : IStateNode
    {
        public string Name { get; private set; }
        public string Value { get; set; }

        public StringValueNode(string name, string initialValue)
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
            Value = readGraph.ReadStringEntry(Name);
        }
    }
}
