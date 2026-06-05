using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class FloatState : IObjectNode
    {
        public float Value;
        public readonly string Name;

        public FloatState(float initialValue, string name = nameof(FloatState))
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteFloat(Value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            Value = stateStream.ReadFloat();
        }

        public string OutputNameAndValue()
        {
            return $"\"{Name}\": {Value}";
        }
    }
}
