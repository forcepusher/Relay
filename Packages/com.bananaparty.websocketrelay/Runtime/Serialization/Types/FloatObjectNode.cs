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

        public void Serialize(IStateNode stateStream)
        {
            stateStream.WriteFloat(Name, Value);
        }

        public void Deserialize(IStateNode stateStream)
        {
            Value = stateStream.ReadFloat(Name);
        }

        public string OutputNameAndValue()
        {
            return Json.ConvertToText(Name, Value);
        }
    }
}
