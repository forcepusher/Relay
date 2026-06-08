using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class ObjectNode : INode
    {
        public string Name { get; }
        private readonly List<INode> _nodes;

        public ObjectNode(string name, List<INode> nodes)
        {
            Name = name;
            _nodes = nodes;
        }

        public List<INode> GetNodes() => _nodes;

        public void WriteStateToJson(JsonWriteStateGraph stateGraph)
        {
            stateGraph.StartObject(Name);

            foreach (INode node in _nodes)
                node.WriteStateToJson(stateGraph);

            stateGraph.EndObject();
        }

        public void ReadStateFromJson(JsonReadStateGraph stateGraph)
        {
            stateGraph.StartObject(Name);

            foreach (INode node in _nodes)
                node.ReadStateFromJson(stateGraph);

            stateGraph.EndObject();
        }
    }
}
