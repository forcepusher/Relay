namespace BananaParty.WebSocketRelay
{
    public class BoolObjectNode : IObjectNode
    {
        public bool Value;
        public readonly string Name;

        public BoolObjectNode(string name, bool initialValue)
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteBool(Name, Value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            Value = stateStream.ReadBool(Name);
        }

        public string OutputNameAndValue()
        {
            return Json.ConvertToText(Name, Value);
        }
    }
}
