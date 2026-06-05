namespace BananaParty.WebSocketRelay
{
    public class State<T>
    {
        private T _value;

        public void Write(T value) => _value = value;
        public T Read() => _value;
    }
}
