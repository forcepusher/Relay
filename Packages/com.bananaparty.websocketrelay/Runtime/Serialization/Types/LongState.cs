namespace BananaParty.WebSocketRelay
{
    public class LongState : IState
    {
        public long Value;
        public readonly string Name;

        public LongState(long initialValue, string name = nameof(LongState))
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteLong(Value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            Value = stateStream.ReadLong();
        }

        public string OutputNameAndValue()
        {
            return $"\"{Name}\": {Value}";
        }
    }
}
