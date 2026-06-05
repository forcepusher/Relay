namespace BananaParty.WebSocketRelay
{
    public class ByteObjectNode : IObjectNode
    {
        public byte Value;
        public readonly string Name;

        public ByteObjectNode(byte initialValue, string name = nameof(ByteObjectNode))
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
            return Json.ConvertToText(Name, Value);
        }
    }
}
