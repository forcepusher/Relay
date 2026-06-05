namespace BananaParty.WebSocketRelay
{
    public class LongState : IState
    {
        private long _value;
        private string _name;

        public LongState(long initialValue, string name = nameof(LongState))
        {
            _value = initialValue;
            _name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteLong(_value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            _value = stateStream.ReadLong();
        }

        public long Value
        {
            get => _value;
            set => _value = value;
        }
    }
}
