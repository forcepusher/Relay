using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class FloatObjectNode : IObjectNode
    {
        public float Value;
        public readonly string Name;

        public FloatObjectNode(string name, float initialValue)
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteFloat(Name, Value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            Value = stateStream.ReadFloat(Name);
        }

        public string OutputNameAndValue()
        {
            return Json.ConvertToText(Name, Value);
        }
    }
}
