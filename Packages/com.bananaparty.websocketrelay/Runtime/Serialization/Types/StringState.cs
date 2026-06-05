namespace BananaParty.WebSocketRelay
{
    public class StringState : IStateNode
    {
        public string Value;
        public readonly string Name;

        public StringState(string initialValue, string name = nameof(StringState))
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
