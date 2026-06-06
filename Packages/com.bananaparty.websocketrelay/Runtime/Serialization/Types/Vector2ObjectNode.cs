using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector2ObjectNode : IObjectNode
    {
        public Vector2 Value;
        public readonly string Name;

        public Vector2ObjectNode(string name, Vector2 initialValue)
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteVector2(Name, Value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            Value = stateStream.ReadVector2(Name);
        }

        public string OutputNameAndValue()
        {
            return Json.ConvertToText(Name, Value);
        }
    }
}
