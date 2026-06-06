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

        public void Serialize(IStateNode stateStream)
        {
            stateStream.WriteBool(Name, Value);
        }

        public void Deserialize(IStateNode stateStream)
        {
            Value = stateStream.ReadBool(Name);
        }

        public string OutputNameAndValue()
        {
            return Json.ConvertToText(Name, Value);
        }
    }
}
