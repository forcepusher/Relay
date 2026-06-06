using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector2IntObjectNode : IObjectNode
    {
        public Vector2Int Value;
        public readonly string Name;

        public Vector2IntObjectNode(string name, Vector2Int initialValue)
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateNode stateStream)
        {
            stateStream.WriteVector2Int(Name, Value);
        }

        public void Deserialize(IStateNode stateStream)
        {
            Value = stateStream.ReadVector2Int(Name);
        }

        public string OutputNameAndValue()
        {
            return Json.ConvertToText(Name, Value);
        }
    }
}
