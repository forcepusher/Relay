using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class ObjectNode : IObjectNode
    {
        public string Name { get; }
        private readonly List<INode> _nodes;

        public ObjectNode(string name, List<INode> nodes)
        {
            Name = name;
            _nodes = nodes;
        }

        public List<INode> GetNodes() => _nodes;

        public void WriteJsonState(JsonStateGraph stateGraph)
        {
            stateGraph.StartChildGroup(Name);

            foreach (INode node in _nodes)
                node.WriteJsonState(stateGraph);

            stateGraph.EndChildGroup();
        }
    }
}
