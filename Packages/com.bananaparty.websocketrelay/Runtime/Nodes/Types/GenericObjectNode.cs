using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class GenericObjectNode : IObjectNode
    {
        public string Name { get; }
        private readonly List<INode> _nodes = new();

        public GenericObjectNode(string name)
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
