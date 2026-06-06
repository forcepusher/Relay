namespace BananaParty.WebSocketRelay
{
    public class ByteObjectNode : IObjectNode
    {
        public byte Value;
        public readonly string Name;

        public ByteObjectNode(string name, byte initialValue)
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateNode stateStream)
        {
            stateStream.WriteByte(Name, Value);
        }

        public void Deserialize(IStateNode stateStream)
        {
            Value = stateStream.ReadByte(Name);
        }

        public string OutputNameAndValue()
        {
            return Json.ConvertToText(Name, Value);
        }
    }
}
