using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class ObjectNode : IObjectNode
    {
        public string Name { get; }
        private readonly List<INode> _nodes;

        public ObjectNode(string name, List<INode> _nodes)
        {
            Name = name;
        }

        public void AddNode(INode node)
        {
            _nodes.Add(node);
        }

        public List<INode> GetNodes() => _nodes;
    }
}
