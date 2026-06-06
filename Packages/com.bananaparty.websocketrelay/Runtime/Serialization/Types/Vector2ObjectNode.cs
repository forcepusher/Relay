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

        public void Serialize(IStateNode stateStream)
        {
            stateStream.WriteVector2(Name, Value);
        }

        public void Deserialize(IStateNode stateStream)
        {
            Value = stateStream.ReadVector2(Name);
        }

        public string OutputNameAndValue()
        {
            return Json.ConvertToText(Name, Value);
        }
    }
}
