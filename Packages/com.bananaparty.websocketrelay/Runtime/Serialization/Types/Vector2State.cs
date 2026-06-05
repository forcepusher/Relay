using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector2State : IStateNode
    {
        public Vector2 Value;
        public readonly string Name;

        public Vector2State(Vector2 initialValue, string name = nameof(Vector2State))
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteVector2(Value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            Value = stateStream.ReadVector2();
        }

        public string OutputNameAndValue()
        {
            return $"\"{Name}\": \"{Value}\"";
        }
    }
}
