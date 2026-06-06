using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector3IntObjectNode : IObjectNode
    {
        private Vector3Int _value;
        private string _name;

        public Vector3IntObjectNode(string name, Vector3Int initialValue)
        {
            _value = initialValue;
            _name = name;
        }

        public Vector3Int Value
        {
            get => _value;
            set => _value = value;
        }
    }
}
