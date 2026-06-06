namespace BananaParty.WebSocketRelay
{
    public class LongObjectNode : IObjectNode
    {
        public long Value;
        public readonly string Name;

        public LongObjectNode(string name, long initialValue)
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteLong(Name, Value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            Value = stateStream.ReadLong(Name);
        }

        public string OutputNameAndValue()
        {
            return Json.ConvertToText(Name, Value);
        }
    }
}
