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

        public void Serialize(IStateNode stateStream)
        {
            stateStream.WriteVector3Int(_name, _value);
        }

        public void Deserialize(IStateNode stateStream)
        {
            _value = stateStream.ReadVector3Int(_name);
        }

        public Vector3Int Value
        {
            get => _value;
            set => _value = value;
        }

        public string OutputNameAndValue()
        {
            return Json.ConvertToText(_name, _value);
        }
    }
}
