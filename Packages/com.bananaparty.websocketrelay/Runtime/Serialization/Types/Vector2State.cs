using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector2State : IState
    {
        private Vector2 _value;
        private string _name;

        public Vector2State(Vector2 initialValue, string name = nameof(Vector2State))
        {
            _value = initialValue;
            _name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteVector2(_value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            _value = stateStream.ReadVector2();
        }

        public Vector2 Value
        {
            get => _value;
            set => _value = value;
        }
    }
}
