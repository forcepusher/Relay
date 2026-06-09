using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class ObjectState : IState
    {
        public string StateName { get; }
        private readonly List<IState> _states;

        public ObjectState(string name, List<IState> states)
        {
            StateName = name;
            _states = states;
        }

        public void WriteState(IStateOutput writeGraph)
        {
            writeGraph.StartObject(StateName);

            foreach (IState state in _states)
                state.WriteState(writeGraph);

            writeGraph.EndObject();
        }

        public void ReadState(IStateInput readGraph)
        {
            readGraph.StartObject(StateName);

            foreach (IState state in _states)
                state.ReadState(readGraph);

            readGraph.EndObject();
        }
    }
}
