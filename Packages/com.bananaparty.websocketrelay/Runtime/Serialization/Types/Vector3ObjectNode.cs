using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector3ObjectNode : IObjectNode
    {
        public Vector3 Value;
        public readonly string Name;

        public Vector3ObjectNode(string name, Vector3 initialValue)
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteVector3(Name, Value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            Value = stateStream.ReadVector3(Name);
        }

        public string OutputNameAndValue()
        {
            return Json.ConvertToText(Name, Value);
        }
    }
}
