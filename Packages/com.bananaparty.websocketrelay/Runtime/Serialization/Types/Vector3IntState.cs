using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector3IntState : IStateObject
    {
        private Vector3Int _value;
        private string _name;

        public Vector3IntState(Vector3Int initialValue, string name = nameof(Vector3IntState))
        {
            _value = initialValue;
            _name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteVector3Int(_value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            _value = stateStream.ReadVector3Int();
        }

        public Vector3Int Value
        {
            get => _value;
            set => _value = value;
        }

        public string OutputNameAndValue()
        {
            return $"\"{_name}\": \"{_value}\"";
        }
    }
}
