using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector2ObjectNode : IObjectNode
    {
        public Vector2 Value;
        public readonly string Name;

        public Vector2ObjectNode(string name, Vector2 initialValue)
        {
            Value = initialValue;
            Name = name;
        }
    }
}
