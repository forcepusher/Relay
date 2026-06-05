using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class BoolState : IState
    {
        public bool Value;
        public readonly string Name;

        public BoolState(bool initialValue, string name = nameof(BoolState))
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteBool(Value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            Value = stateStream.ReadBool();
        }
    }
}
