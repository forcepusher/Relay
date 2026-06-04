namespace BananaParty.WebSocketRelay
{
    public class State<T>
    {
        public bool HasValue { get; private set; } = false;
        private T _value;

        public T Read()
        {
            if (!HasValue)
                throw InvalidOperationException($"Attempt to {nameof(Read)} while {nameof(HasValue)} = {HasValue}");

            return _value;
        }

        public Write(T value)
        {
            _hasValue = true;
            _value = value;
        }
    }
}
