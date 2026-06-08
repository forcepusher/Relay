using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class ArrayNode<T> : IJsonState where T : IJsonState
    {
        public string Name { get; }
        private readonly List<T> _nodes;

        public ArrayNode(string name, List<T> nodes)
        {
            Name = name;
            _nodes = nodes;
        }

        public void WriteStateToJson(JsonWriteGraph stateGraph)
        {
            stateGraph.StartArray(Name);

            foreach (T node in _nodes)
                node.WriteStateToJson(stateGraph);

            stateGraph.EndArray();
        }

        public void ReadStateFromJson(JsonReadGraph stateGraph)
        {
            stateGraph.StartArray(Name);

            foreach (T node in _nodes)
                node.ReadStateFromJson(stateGraph);

            stateGraph.EndArray();
        }
    }
}
