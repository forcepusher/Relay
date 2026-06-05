namespace BananaParty.WebSocketRelay
{
    public class BoolObjectNode : IObjectNode
    {
        public bool Value;
        public readonly string Name;

        public BoolObjectNode(bool initialValue, string name = nameof(BoolObjectNode))
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteBool(Value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            Value = stateStream.ReadBool();
        }

        public string OutputNameAndValue()
        {
            return $"\"{Name}\": {Value.ToString().ToLower()}";
        }
    }
}
