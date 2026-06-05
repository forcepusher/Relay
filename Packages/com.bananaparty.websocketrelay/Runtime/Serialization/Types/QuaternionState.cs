using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class QuaternionState : IState
    {
        private Quaternion _value;
        private string _name;

        public QuaternionState(Quaternion initialValue, string name = nameof(QuaternionState))
        {
            _value = initialValue;
            _name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteQuaternion(_value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            _value = stateStream.ReadQuaternion();
        }

        public Quaternion Value
        {
            get => _value;
            set => _value = value;
        }
    }
}
