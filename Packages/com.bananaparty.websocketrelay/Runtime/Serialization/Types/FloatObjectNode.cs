using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class FloatObjectNode : IObjectNode
    {
        public float Value;
        public readonly string Name;

        public FloatObjectNode(string name, float initialValue)
        {
            Value = initialValue;
            Name = name;
        }
    }
}
