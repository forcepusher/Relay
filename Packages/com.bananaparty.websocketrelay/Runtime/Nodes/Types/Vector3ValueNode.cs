using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector3ValueNode : IStateNode
    {
        public string Name { get; private set; }
        public Vector3 Value { get; set; }

        public Vector3ValueNode(string name, Vector3 initialValue)
        {
            Name = name;
            Value = initialValue;
        }

        public void Write(IWriteGraph writeGraph)
        {
            writeGraph.StartObject(Name);
            writeGraph.WriteEntry("x", Value.x);
            writeGraph.WriteEntry("y", Value.y);
            writeGraph.WriteEntry("z", Value.z);
            writeGraph.EndObject();
        }

        public void Read(IReadGraph readGraph)
        {
            readGraph.StartObject(Name);
            Value = new Vector3(
                readGraph.ReadFloatEntry("x"),
                readGraph.ReadFloatEntry("y"),
                readGraph.ReadFloatEntry("z"));
            readGraph.EndObject();
        }
    }
}
