using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class ArrayNode : IObjectNode
    {
        public string Name { get; }
        private readonly List<INode> _nodes;

        public ArrayNode(string name, List<INode> nodes)
        {
            Name = name;
            _nodes = nodes;
        }

        public List<INode> GetNodes() => _nodes;

        public void WriteStateToJson(JsonWriteStateGraph stateGraph)
        {
            stateGraph.StartChildArray(Name);

            foreach (INode node in _nodes)
                node.WriteStateToJson(stateGraph);

            stateGraph.EndChildArray();
        }

        public void ReadStateFromJson(JsonReadStateGraph stateGraph)
        {
            stateGraph.StartChildArray(Name);

            foreach (INode node in _nodes)
                node.ReadStateFromJson(stateGraph);

            stateGraph.EndChildArray();
        }
    }
}
