namespace BananaParty.WebSocketRelay
{
    public class LongObjectNode : IObjectNode
    {
        public long Value;
        public readonly string Name;

        public LongObjectNode(long initialValue, string name = nameof(LongObjectNode))
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteLong(Value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            Value = stateStream.ReadLong();
        }

        public string OutputNameAndValue()
        {
            return $"\"{Name}\": {Value}";
        }
    }
}
