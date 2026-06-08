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

        public void Write(IWriteGraph writeGraph)
        {
            writeGraph.StartArray(Name);

            foreach (T state in _states)
                state.Write(writeGraph);

            writeGraph.EndArray();
        }

        public void Read(IReadGraph readGraph)
        {
            readGraph.StartArray(Name);

            foreach (T state in _states)
                state.Read(readGraph);

            readGraph.EndArray();
        }
    }
}
