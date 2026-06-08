using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class ArrayNode<T> : IStateNode where T : IStateNode
    {
        public string Name { get; }
        private readonly List<T> _nodes;

        public ArrayNode(string name, List<T> nodes)
        {
            Name = name;
            _nodes = nodes;
        }

        public void Write(IWriteGraph writeGraph)
        {
            writeGraph.StartArray(Name);

            foreach (T node in _nodes)
                node.Write(writeGraph);

            writeGraph.EndArray();
        }

        public void Read(IReadGraph readGraph)
        {
            readGraph.StartArray(Name);

            foreach (T node in _nodes)
                node.Read(readGraph);

            readGraph.EndArray();
        }
    }
}
