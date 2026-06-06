using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector2IntObjectNode : IObjectNode
    {
        public Vector2Int Value;
        public readonly string Name;

        public Vector2IntObjectNode(string name, Vector2Int initialValue)
        {
            Value = initialValue;
            Name = name;
        }
    }
}
