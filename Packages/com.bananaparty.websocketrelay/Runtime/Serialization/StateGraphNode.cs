using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class StateGraphNode
    {
        private readonly List<StateGraphNode> _children = new();
        private readonly List<IState> _states = new();

        public void AddChildStateGraphNode(StateGraphNode stateGraphNode)
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

            foreach (StateGraphNode stateGraphNode in _children)
                stateGraphNode.Serialize(stateStream);
        }

        public void Deserialize(IStateStream stateStream)
        {
            foreach (IState state in _states)
                state.Deserialize(stateStream);

            foreach (StateGraphNode stateGraphNode in _children)
                stateGraphNode.Deserialize(stateStream);
        }
    }
}
