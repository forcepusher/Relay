using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector3ObjectNode : IValueNode
    {
        public string Name { get; private set; }
        public Vector3 Value;

        public Vector3ObjectNode(string name, Vector3 initialValue)
        {
            Value = initialValue;
            Name = name;
        }
    }
}
