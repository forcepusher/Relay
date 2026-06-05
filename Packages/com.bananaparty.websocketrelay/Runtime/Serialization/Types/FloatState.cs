using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class FloatState : IState
    {
        private float _value;
        private string _name;

        public FloatState(float initialValue, string name = nameof(FloatState))
        {
            _value = initialValue;
            _name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteFloat(_value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            _value = stateStream.ReadFloat();
        }

        public float Value
        {
            get => _value;
            set => _value = value;
        }
    }
}
