namespace BananaParty.WebSocketRelay
{
    public class ByteState : IStateNode
    {
        public byte Value;
        public readonly string Name;

        public ByteState(byte initialValue, string name = nameof(ByteState))
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteByte(Value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            Value = stateStream.ReadByte();
        }

        public string OutputNameAndValue()
        {
            return $"\"{Name}\": {Value}";
        }
    }
}
