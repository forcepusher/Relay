using System;

namespace BananaParty.WebSocketRelay
{
    public class State<T>
    {
        public bool HasValue { get; private set; } = false;
        private T _value;

        public T Read()
        {
            if (!HasValue)
                throw new InvalidOperationException($"Attempt to {nameof(Read)} while {nameof(HasValue)} = {HasValue}");

            return _value;
        }

        public void Write(T value)
        {
            HasValue = true;
            _value = value;
        }
    }
}
