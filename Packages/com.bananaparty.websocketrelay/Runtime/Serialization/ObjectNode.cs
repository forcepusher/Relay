using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class ObjectNode : IObjectNode
    {
        public string Name { get; private set; }

        private readonly IObjectNode[] _childObjectNodes;

        public ObjectNode(string name, params IObjectNode[] childObjectNodes)
        {
            Name = name;
            _childObjectNodes = childObjectNodes;
        }

        public void Serialize(IStateStream stateStream)
        {
            foreach (IObjectNode childObjectNode in _childObjectNodes)
                childObjectNode.Serialize(stateStream);
        }

        public void Deserialize(IStateStream stateStream)
        {
            foreach (IObjectNode childObjectNode in _childObjectNodes)
                childObjectNode.Deserialize(stateStream);
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
