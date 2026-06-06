using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector4ObjectNode : IObjectNode
    {
        public Vector4 Value;
        public readonly string Name;

        public Vector4ObjectNode(string name, Vector4 initialValue)
        {
            Value = initialValue;
            Name = name;
        }
    }
}
