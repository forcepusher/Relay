using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector2IntState : IState
    {
        private Vector2Int _value;
        private string _name;

        public Vector2IntState(Vector2Int initialValue, string name = nameof(Vector2IntState))
        {
            _value = initialValue;
            _name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteVector2Int(_value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            _value = stateStream.ReadVector2Int();
        }

        public Vector2Int Value
        {
            get => _value;
            set => _value = value;
        }
    }
}
