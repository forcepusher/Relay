namespace BananaParty.WebSocketRelay
{
    public class State<T> : IState
    {
        private T _currentValue;
        private T _serializedValue;

        public State(T initialValue)
        {
            _currentValue = initialValue;
            _serializedValue = initialValue;
        }

        public void Serialize()
        {
            _serializedValue = _currentValue;
        }

        public void Deserialize()
        {
            _currentValue = _serializedValue;
        }

        public void Write(T value) => _currentValue = value;
        public T Read() => _currentValue;
    }
}
