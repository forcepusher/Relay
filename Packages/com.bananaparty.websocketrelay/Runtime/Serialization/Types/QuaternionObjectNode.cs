using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class QuaternionObjectNode : IObjectNode
    {
        public Quaternion Value;
        public readonly string Name;

        public QuaternionObjectNode(string name, Quaternion initialValue)
        {
            Value = initialValue;
            Name = name;
        }
    }
}
