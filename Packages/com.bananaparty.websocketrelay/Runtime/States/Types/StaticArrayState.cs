using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class StaticArrayState<T> : IState where T : IState
    {
        public string StateName { get; }
        private readonly List<T> _states;

        public StaticArrayState(string name, List<T> states)
        {
            StateName = name;
            _states = states;
        }

        public void WriteState(IStateOutput writeGraph)
        {
            writeGraph.StartArray(StateName);

            foreach (T state in _states)
                state.WriteState(writeGraph);

            writeGraph.EndArray();
        }

        public void ReadState(IStateInput readGraph)
        {
            readGraph.StartArray(StateName);

            foreach (T state in _states)
                state.ReadState(readGraph);

            readGraph.EndArray();
        }
    }
}
