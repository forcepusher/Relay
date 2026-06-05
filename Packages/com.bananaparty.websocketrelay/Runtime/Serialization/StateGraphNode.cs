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

        public void Serialize()
        {
            foreach (IState state in _states)
                state.Serialize();
        }

        public void Deserialize()
        {
            foreach (IState state in _states)
                state.Deserialize();
        }
    }
}
