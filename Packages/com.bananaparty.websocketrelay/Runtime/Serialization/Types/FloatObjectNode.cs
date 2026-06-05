using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class FloatObjectNode : IObjectNode
    {
        public float Value;
        public readonly string Name;

        public FloatObjectNode(float initialValue, string name = nameof(FloatObjectNode))
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
            return Json.ConvertToText(Name, Value);
        }
    }
}
