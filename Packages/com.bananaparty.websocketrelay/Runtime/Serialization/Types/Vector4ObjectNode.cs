using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector4ObjectNode : IObjectNode
    {
        public Vector4 Value;
        public readonly string Name;

        public Vector4ObjectNode(string name, Vector4 initialValue)
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateNode stateStream)
        {
            stateStream.WriteVector4(Name, Value);
        }

        public void Deserialize(IStateNode stateStream)
        {
            Value = stateStream.ReadVector4(Name);
        }

        public string OutputNameAndValue()
        {
            return Json.ConvertToText(Name, Value);
        }
    }
}
