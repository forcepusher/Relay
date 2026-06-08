using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class ObjectNode : IStateNode
    {
        public string Name { get; }
        private readonly List<IStateNode> _nodes;

        public ObjectNode(string name, List<IStateNode> nodes)
        {
            Name = name;
            _nodes = nodes;
        }

        public void Write(IWriteGraph writeGraph)
        {
            writeGraph.StartObject(Name);

            foreach (IStateNode node in _nodes)
                node.Write(writeGraph);

            writeGraph.EndObject();
        }

        public void Read(IReadGraph readGraph)
        {
            readGraph.StartObject(Name);

            foreach (IStateNode node in _nodes)
                node.Read(readGraph);

            readGraph.EndObject();
        }
    }
}
