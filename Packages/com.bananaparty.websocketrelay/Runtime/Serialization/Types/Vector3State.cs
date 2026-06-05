using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector3State : IStateObject
    {
        public Vector3 Value;
        public readonly string Name;

        public Vector3State(Vector3 initialValue, string name = nameof(Vector3State))
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteVector3(Value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            Value = stateStream.ReadVector3();
        }

        public string OutputNameAndValue()
        {
            return $"\"{Name}\": \"{Value}\"";
        }
    }
}
