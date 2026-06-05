namespace BananaParty.WebSocketRelay
{
    public class IntegerState : IStateNode
    {
        public int Value;
        public readonly string Name;

        public IntegerState(int initialValue, string name = nameof(IntegerState))
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteInt(Value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            Value = stateStream.ReadInt();
        }

        public string OutputNameAndValue()
        {
            return $"\"{Name}\": {Value}";
        }
    }
}
