using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector2IntObjectNode : IObjectNode
    {
        public Vector2Int Value;
        public readonly string Name;

        public Vector2IntObjectNode(Vector2Int initialValue, string name = nameof(Vector2IntObjectNode))
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteVector2Int(Value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            Value = stateStream.ReadVector2Int();
        }

        public string OutputNameAndValue()
        {
            return Json.ConvertToText(Name, Value);
        }
    }
}
