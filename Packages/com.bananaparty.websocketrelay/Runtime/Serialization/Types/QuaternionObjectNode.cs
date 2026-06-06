using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class QuaternionObjectNode : IObjectNode
    {
        public Quaternion Value;
        public readonly string Name;

        public QuaternionObjectNode(string name, Quaternion initialValue)
        {
            Value = initialValue;
            Name = name;
        }

        public void Serialize(IStateNode stateStream)
        {
            stateStream.WriteQuaternion(Name, Value);
        }

        public void Deserialize(IStateNode stateStream)
        {
            Value = stateStream.ReadQuaternion(Name);
        }

        public string OutputNameAndValue()
        {
            return Json.ConvertToText(Name, Value);
        }
    }
}
