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

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteVector4(Name, Value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            Value = stateStream.ReadVector4(Name);
        }

        public string OutputNameAndValue()
        {
            return Json.ConvertToText(Name, Value);
        }
    }
}
