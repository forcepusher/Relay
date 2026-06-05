using System;

namespace BananaParty.WebSocketRelay
{
    public class EnumObjectNode<T> : IObjectNode where T : Enum
    {
        public T Value;
        public readonly string Name;

        public EnumObjectNode(T initialValue, string name = nameof(EnumObjectNode<T>))
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
            return Json.ConvertToText(Name, Value);
        }
    }
}
