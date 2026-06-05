using System;

namespace BananaParty.WebSocketRelay
{
    public class EnumState<T> : IState where T : Enum
    {
        private T _value;
        private string _name;

        public EnumState(T initialValue, string name = nameof(EnumState<T>))
        {
            _value = initialValue;
            _name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteEnum(_value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            _value = stateStream.ReadEnum<T>();
        }

        public T Value
        {
            get => _value;
            set => _value = value;
        }
    }
}
