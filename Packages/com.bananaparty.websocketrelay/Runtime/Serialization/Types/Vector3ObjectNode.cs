using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector3ObjectNode : IObjectNode
    {
        public Vector3 Value;
        public readonly string Name;

        public Vector3ObjectNode(Vector3 initialValue, string name = nameof(Vector3ObjectNode))
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
            return Json.ConvertToText(Name, Value);
        }
    }
}
