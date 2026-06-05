using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class QuaternionState : IStateObject
    {
        public Quaternion Value;
        public readonly string Name;

        public QuaternionState(Quaternion initialValue, string name = nameof(QuaternionState))
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteQuaternion(Value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            Value = stateStream.ReadQuaternion();
        }

        public string OutputNameAndValue()
        {
            return $"\"{Name}\": \"{Value}\"";
        }
    }
}
