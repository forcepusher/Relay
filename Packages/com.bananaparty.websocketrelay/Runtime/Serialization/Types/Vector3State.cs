using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector3State : IState
    {
        private Vector3 _value;
        private string _name;

        public Vector3State(Vector3 initialValue, string name = nameof(Vector3State))
        {
            _value = initialValue;
            _name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteVector3(_value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            _value = stateStream.ReadVector3();
        }

        public Vector3 Value
        {
            get => _value;
            set => _value = value;
        }
    }
}
