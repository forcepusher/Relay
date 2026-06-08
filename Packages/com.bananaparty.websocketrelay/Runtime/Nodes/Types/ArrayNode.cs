using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class ArrayNode<T> : IObjectNode where T : INode
    {
        public string Name { get; }
        private readonly List<T> _nodes;

        public ArrayNode(string name, List<T> nodes)
        {
            Name = name;
            _nodes = nodes;
        }

        public List<INode> GetNodes()
        {
            List<INode> nodes = new(_nodes.Count);
            foreach (T node in _nodes)
                nodes.Add(node);
            return nodes;
        }

        public void WriteStateToJson(JsonWriteStateGraph stateGraph)
        {
            stateGraph.StartChildArray(Name);

            foreach (T node in _nodes)
                node.WriteStateToJson(stateGraph);

            stateGraph.EndChildArray();
        }

        public void ReadStateFromJson(JsonReadStateGraph stateGraph)
        {
            stateGraph.StartChildArray(Name);

            foreach (T node in _nodes)
                node.ReadStateFromJson(stateGraph);

            stateGraph.EndChildArray();
        }
    }

    public class ArrayNode : ArrayNode<INode>
    {
        public ArrayNode(string name, List<INode> nodes) : base(name, nodes) { }
    }
}
