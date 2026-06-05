using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class QuaternionObjectNode : IObjectNode
    {
        public Quaternion Value;
        public readonly string Name;

        public QuaternionObjectNode(Quaternion initialValue, string name = nameof(QuaternionObjectNode))
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
