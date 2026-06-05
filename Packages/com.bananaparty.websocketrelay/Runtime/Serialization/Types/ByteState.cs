namespace BananaParty.WebSocketRelay
{
    public class ByteState : IState
    {
        private byte _value;
        private string _name;

        public ByteState(byte initialValue, string name = nameof(ByteState))
        {
            _value = initialValue;
            _name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteByte(_value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            _value = stateStream.ReadByte();
        }

        public byte Value
        {
            get => _value;
            set => _value = value;
        }
    }
}
