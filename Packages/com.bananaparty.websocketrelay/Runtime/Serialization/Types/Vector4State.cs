using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector4State : IStateNode
    {
        public Vector4 Value;
        public readonly string Name;

        public Vector4State(Vector4 initialValue, string name = nameof(Vector4State))
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteVector4(Value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            Value = stateStream.ReadVector4();
        }

        public string OutputNameAndValue()
        {
            return $"\"{Name}\": \"{Value}\"";
        }
    }
}
