using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class DynamicArrayState<T> : IState where T : IState
    {
        public string StateName { get; }
        private readonly List<T> _states;

        public DynamicArrayState(string name, List<T> states)
        {
            StateName = name;
            _states = states;
        }

        public void WriteState(IStateOutput writeGraph)
        {
            writeGraph.StartArray(StateName);
            writeGraph.WriteEntry(_states.Count);

            foreach (T state in _states)
                state.WriteState(writeGraph);

            writeGraph.EndArray();
        }

        public void ReadState(IStateInput readGraph)
        {
            readGraph.StartArray(StateName);
            int count = readGraph.ReadIntArrayEntry();

            while (_states.Count > count)
                _states.RemoveAt(_states.Count - 1);

            for (int i = 0; i < count; i++)
                _states[i].ReadState(readGraph);

            readGraph.EndArray();
        }
    }
}
