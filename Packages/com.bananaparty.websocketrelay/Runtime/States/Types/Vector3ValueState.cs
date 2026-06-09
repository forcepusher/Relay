using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector3ValueState : IState
    {
        public string StateName { get; private set; }
        public Vector3 Value { get; set; }

        public Vector3ValueState(string name, Vector3 initialValue)
        {
            StateName = name;
            Value = initialValue;
        }

        public void WriteState(IStateOutput writeGraph)
        {
            writeGraph.StartObject(StateName);
            writeGraph.WriteEntry("x", Value.x);
            writeGraph.WriteEntry("y", Value.y);
            writeGraph.WriteEntry("z", Value.z);
            writeGraph.EndObject();
        }

        public void ReadState(IStateInput readGraph)
        {
            readGraph.StartObject(StateName);
            Value = new Vector3(
                readGraph.ReadFloat("x"),
                readGraph.ReadFloat("y"),
                readGraph.ReadFloat("z"));
            readGraph.EndObject();
        }
    }
}
