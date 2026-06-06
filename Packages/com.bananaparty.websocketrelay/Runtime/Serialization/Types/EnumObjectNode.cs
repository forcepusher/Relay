using System;

namespace BananaParty.WebSocketRelay
{
    public class EnumObjectNode<T> : IObjectNode where T : Enum
    {
        public T Value;
        public readonly string Name;

        public EnumObjectNode(string name, T initialValue)
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateNode stateStream)
        {
            stateStream.WriteEnum(Name, Value);
        }

        public void Deserialize(IStateNode stateStream)
        {
            Value = stateStream.ReadEnum<T>(Name);
        }

        public string OutputNameAndValue()
        {
            return Json.ConvertToText(Name, Value);
        }
    }
}
