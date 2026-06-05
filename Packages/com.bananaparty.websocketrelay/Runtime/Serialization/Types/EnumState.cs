using System;

namespace BananaParty.WebSocketRelay
{
    public class EnumState<T> : IStateObject where T : Enum
    {
        public T Value;
        public readonly string Name;

        public EnumState(T initialValue, string name = nameof(EnumState<T>))
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteEnum(Value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            Value = stateStream.ReadEnum<T>();
        }

        public string OutputNameAndValue()
        {
            return $"\"{Name}\": \"{Value}\"";
        }
    }
}
