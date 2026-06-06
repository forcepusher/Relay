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
    }
}
