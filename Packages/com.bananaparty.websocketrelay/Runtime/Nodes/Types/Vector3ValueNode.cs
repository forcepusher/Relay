using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector3ValueNode : IValueNode<Vector3>
    {
        public string Name { get; private set; }
        public Vector3 Value { get; set; }

        public Vector3ValueNode(string name, Vector3 initialValue)
        {
            Name = name;
            Value = initialValue;
        }

        public void WriteStateToJson(JsonStateGraph stateGraph)
        {
            stateGraph.WriteEntry(Name, Value.ToString(), true);
        }
    }
}
