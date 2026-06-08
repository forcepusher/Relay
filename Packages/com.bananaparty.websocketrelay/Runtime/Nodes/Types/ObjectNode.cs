using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class ObjectNode : IJsonState
    {
        public string Name { get; }
        private readonly List<IJsonState> _nodes;

        public ObjectNode(string name, List<IJsonState> nodes)
        {
            Name = name;
            _nodes = nodes;
        }

        public void WriteToJson(JsonWriteGraph stateGraph)
        {
            stateGraph.StartObject(Name);

            foreach (IJsonState node in _nodes)
                node.WriteToJson(stateGraph);

            stateGraph.EndObject();
        }

        public void ReadFromJson(JsonReadGraph stateGraph)
        {
            stateGraph.StartObject(Name);

            foreach (IJsonState node in _nodes)
                node.ReadFromJson(stateGraph);

            stateGraph.EndObject();
        }
    }
}
