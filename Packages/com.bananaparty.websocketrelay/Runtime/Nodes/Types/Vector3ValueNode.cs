using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector3ValueNode : IBinaryState
    {
        public string Name { get; private set; }
        public Vector3 Value { get; set; }

        public Vector3ValueNode(string name, Vector3 initialValue)
        {
            Name = name;
            Value = initialValue;
        }

        public void WriteStateToJson(JsonWriteGraph stateGraph)
        {
            stateGraph.StartObject(Name);
            stateGraph.WriteEntry("x", Value.x);
            stateGraph.WriteEntry("y", Value.y);
            stateGraph.WriteEntry("z", Value.z);
            stateGraph.EndObject();
        }

        public void ReadStateFromJson(JsonReadGraph stateGraph)
        {
            stateGraph.StartObject(Name);
            Value = new Vector3(
                stateGraph.ReadFloatEntry("x"),
                stateGraph.ReadFloatEntry("y"),
                stateGraph.ReadFloatEntry("z"));
            stateGraph.EndObject();
        }

        public void WriteToBinary(BinaryWriteGraph stateGraph)
        {
            stateGraph.StartObject(Name);
            stateGraph.WriteEntry("x", Value.x);
            stateGraph.WriteEntry("y", Value.y);
            stateGraph.WriteEntry("z", Value.z);
            stateGraph.EndObject();
        }

        public void ReadFromBinary(BinaryReadGraph stateGraph)
        {
            stateGraph.StartObject(Name);
            Value = new Vector3(
                stateGraph.ReadFloatEntry("x"),
                stateGraph.ReadFloatEntry("y"),
                stateGraph.ReadFloatEntry("z"));
            stateGraph.EndObject();
        }
    }
}
