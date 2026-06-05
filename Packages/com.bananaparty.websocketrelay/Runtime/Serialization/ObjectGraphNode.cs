using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class ObjectGraphNode : IStateGraphNode
    {
        public string Name { get; private set; }

        private readonly List<IState> _states = new();
        private readonly List<IStateGraphNode> _children = new();

        public ObjectGraphNode(string name)
        {
            Name = name;
        }

        public void AddChildStateGraphNode(IStateGraphNode stateGraphNode)
        {
            _children.Add(stateGraphNode);
        }

        public void AddState(IState state)
        {
            _states.Add(state);
        }

        public void Serialize(IStateStream stateStream)
        {
            foreach (IState state in _states)
                state.Serialize(stateStream);

            foreach (IStateGraphNode stateGraphNode in _children)
                stateGraphNode.Serialize(stateStream);
        }

        public void Deserialize(IStateStream stateStream)
        {
            foreach (IState state in _states)
                state.Deserialize(stateStream);

            foreach (IStateGraphNode stateGraphNode in _children)
                stateGraphNode.Deserialize(stateStream);
        }

        public string OutputNameAndValue()
        {
            var sb = new System.Text.StringBuilder();
            sb.Append("{");

            sb.Append("\"states\": {");
            for (int i = 0; i < _states.Count; i++)
            {
                sb.Append(_states[i].OutputNameAndValue());
                if (i < _states.Count - 1) sb.Append(", ");
            }
            sb.Append("}, ");

            sb.Append("\"children\": [");
            for (int i = 0; i < _children.Count; i++)
            {
                sb.Append(_children[i].OutputNameAndValue());
                if (i < _children.Count - 1) sb.Append(", ");
            }
            sb.Append("]");

            sb.Append("}");
            return sb.ToString();
        }
    }
}
