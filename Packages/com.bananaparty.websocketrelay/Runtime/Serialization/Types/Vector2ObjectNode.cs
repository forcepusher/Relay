using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector2ObjectNode : IObjectNode
    {
        public Vector2 Value;
        public readonly string Name;

        public Vector2ObjectNode(Vector2 initialValue, string name = nameof(Vector2ObjectNode))
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
            return Json.ConvertToText(Name, Value);
        }
    }
}
