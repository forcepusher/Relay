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

        public void WriteStateToJson(JsonWriteStateGraph stateGraph)
        {
            stateGraph.WriteEntry(Name, Value.ToString(), true);
        }

        public void ReadStateFromJson(JsonReadStateGraph stateGraph)
        {
            string val = stateGraph.ReadEntry(Name);
            if (string.IsNullOrEmpty(val)) return;

            try
            {
                // Expects format like "(0.0, 1.0, 2.0)"
                val = val.Trim('(', ')');
                string[] parts = val.Split(',');
                if (parts.Length == 3)
                {
                    Value = new Vector3(
                        float.Parse(parts[0]),
                        float.Parse(parts[1]),
                        float.Parse(parts[2])
                    );
                }
            }
            catch
            {
                // Log or ignore parse error
            }
        }
    }
}
