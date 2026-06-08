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

        public void WriteStateToJson(JsonWriteGraph stateGraph)
        {
            stateGraph.StartObject(Name);

            foreach (IJsonState node in _nodes)
                node.WriteStateToJson(stateGraph);

            stateGraph.EndObject();
        }

        public void ReadStateFromJson(JsonReadGraph stateGraph)
        {
            stateGraph.StartObject(Name);

            foreach (IJsonState node in _nodes)
                node.ReadStateFromJson(stateGraph);

            stateGraph.EndObject();
        }
    }
}
