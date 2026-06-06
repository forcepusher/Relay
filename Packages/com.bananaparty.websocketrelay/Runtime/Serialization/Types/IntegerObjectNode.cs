namespace BananaParty.WebSocketRelay
{
    public class IntegerObjectNode : IObjectNode
    {
        public int Value;
        public readonly string Name;

        public IntegerObjectNode(string name, int initialValue)
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
            Value = stateStream.ReadInt(Name);
        }

        public string OutputNameAndValue()
        {
            return Json.ConvertToText(Name, Value);
        }
    }
}
