using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class ObjectNode : IStateObject
    {
        public string Name { get; private set; }

        private readonly IStateObject[] _children;

        public ObjectNode(string name, params IStateObject[] children)
        {
            Name = name;
            _children = children;
        }

        public void AddStateNode(IStateObject state)
        {
            _children.Add(state);
        }

        public void Serialize(IStateStream stateStream)
        {
            foreach (IStateObject stateGraphNode in _children)
                stateGraphNode.Serialize(stateStream);
        }

        public void Deserialize(IStateStream stateStream)
        {
            foreach (IStateObject stateGraphNode in _children)
                stateGraphNode.Deserialize(stateStream);
        }

        public string OutputNameAndValue()
        {
            var sb = new System.Text.StringBuilder();
            // sb.Append("{");

            // sb.Append("\"states\": {");
            // for (int i = 0; i < _states.Count; i++)
            // {
            //     sb.Append(_states[i].OutputNameAndValue());
            //     if (i < _states.Count - 1) sb.Append(", ");
            // }
            // sb.Append("}, ");

            // sb.Append("\"children\": [");
            // for (int i = 0; i < _children.Count; i++)
            // {
            //     sb.Append(_children[i].OutputNameAndValue());
            //     if (i < _children.Count - 1) sb.Append(", ");
            // }
            // sb.Append("]");

            // sb.Append("}");
            return sb.ToString();
        }
    }
}
