using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class DynamicArrayState<T> : IState where T : IState
    {
        public string Name { get; }
        private readonly List<T> _states;

        public DynamicArrayState(string name, List<T> states)
        {
            Name = name;
            _states = states;
        }

        public void Write(IStateOutput writeGraph)
        {
            writeGraph.StartArray(Name);
            writeGraph.WriteEntry(_states.Count);

            foreach (T state in _states)
                state.Write(writeGraph);

            writeGraph.EndArray();
        }

        public void Read(IStateInput readGraph)
        {
            readGraph.StartArray(Name);
            int count = readGraph.ReadIntArrayEntry();

            while (_states.Count > count)
                _states.RemoveAt(_states.Count - 1);

            for (int i = 0; i < count; i++)
                _states[i].Read(readGraph);

            readGraph.EndArray();
        }
    }
}
