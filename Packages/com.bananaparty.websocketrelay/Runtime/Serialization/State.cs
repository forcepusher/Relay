namespace BananaParty.WebSocketRelay
{
    public class State<T> : IState
    {
        private T _currentValue;
        private T _savedValue;

        public State(T initialValue)
        {
            _currentValue = initialValue;
            _savedValue = initialValue;
        }

        public void Save()
        {
            _savedValue = _currentValue;
        }

        public void Load()
        {
            _currentValue = _savedValue;
        }

        public void Write(T value) => _currentValue = value;
        public T Read() => _currentValue;
    }
}
