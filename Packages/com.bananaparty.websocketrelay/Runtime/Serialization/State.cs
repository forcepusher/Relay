namespace BananaParty.WebSocketRelay
{
    public class IntegerState : IState
    {
        private int _value;

        public IntegerState(int initialValue)
        {
            _value = initialValue;
        }

        public void Serialize(IStateStream stateStream)
        {
            _serializedValue = _value;
        }

        public void Deserialize()
        {
            _value = _serializedValue;
        }
    }
}
