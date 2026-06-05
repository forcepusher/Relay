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
            sb.Append($"\"{Name}\": {{");
            for (int i = 0; i < _childObjectNodes.Length; i++)
            {
                sb.Append(_childObjectNodes[i].OutputNameAndValue());
                if (i < _childObjectNodes.Length - 1) sb.Append(", ");
            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}
