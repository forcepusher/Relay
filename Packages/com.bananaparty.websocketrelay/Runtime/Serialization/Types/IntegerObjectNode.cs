namespace BananaParty.WebSocketRelay
{
    public class IntegerObjectNode : IObjectNode
    {
        public int Value;
        public readonly string Name;

        public IntegerObjectNode(int initialValue, string name = nameof(IntegerObjectNode))
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteInt(Name, Value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            Value = stateStream.ReadInt();
        }

        public string OutputNameAndValue()
        {
            return Json.ConvertToText(Name, Value);
        }
    }
}
