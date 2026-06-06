namespace BananaParty.WebSocketRelay
{
    public class StringObjectNode : IObjectNode
    {
        public string Value;
        public readonly string Name;

        public StringObjectNode(string name, string initialValue)
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateNode stateStream)
        {
            stateStream.WriteString(Name, Value);
        }

        public void Deserialize(IStateNode stateStream)
        {
            Value = stateStream.ReadString(Name);
        }

        public string OutputNameAndValue()
        {
            return Json.ConvertToText(Name, Value);
        }
    }
}
