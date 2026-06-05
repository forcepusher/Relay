using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class BoolState : IState
    {
        private bool _value;
        private string _name;

        public BoolState(bool initialValue, string name = nameof(BoolState))
        {
            _value = initialValue;
            _name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteBool(_value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            _value = stateStream.ReadBool();
        }

        public bool Value
        {
            get => _value;
            set => _value = value;
        }
    }
}
