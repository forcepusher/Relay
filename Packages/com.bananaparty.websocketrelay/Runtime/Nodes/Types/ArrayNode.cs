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

        public void WriteToJson(JsonWriteGraph stateGraph)
        {
            stateGraph.StartArray(Name);

            foreach (T node in _nodes)
                node.WriteToJson(stateGraph);

            stateGraph.EndArray();
        }

        public void ReadFromJson(JsonReadGraph stateGraph)
        {
            stateGraph.StartArray(Name);

            foreach (T node in _nodes)
                node.ReadFromJson(stateGraph);

            stateGraph.EndArray();
        }
    }
}
