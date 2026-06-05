namespace BananaParty.WebSocketRelay
{
    public class StringObjectNode : IObjectNode
    {
        public string Value;
        public readonly string Name;

        public StringObjectNode(string initialValue, string name = nameof(StringObjectNode))
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteString(Value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            Value = stateStream.ReadString();
        }

        public string OutputNameAndValue()
        {
            string escapedValue = Value?.Replace("\\", "\\\\").Replace("\"", "\\\"");
            return $"\"{Name}\": \"{escapedValue}\"";
        }
    }
}
