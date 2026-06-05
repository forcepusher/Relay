namespace BananaParty.WebSocketRelay
{
    public class StringState : IState
    {
        private string _value;
        private string _name;

        public StringState(string initialValue, string name = nameof(StringState))
        {
            _value = initialValue;
            _name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteString(_value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            _value = stateStream.ReadString();
        }

        public string Value
        {
            get => _value;
            set => _value = value;
        }
    }
}
