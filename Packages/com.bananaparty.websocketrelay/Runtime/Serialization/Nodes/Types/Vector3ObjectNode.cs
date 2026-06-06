using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector3ObjectNode : IValueNode<Vector3>
    {
        public string Name { get; private set; }
        public Vector3 Value { get; set; }

        public Vector3ObjectNode(string name, Vector3 initialValue)
        {
            Name = name;
            Value = initialValue;
        }
    }
}
