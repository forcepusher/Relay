using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class ObjectState : IState
    {
        public string Name { get; }
        private readonly List<IState> _states;

        public ObjectState(string name, List<IState> states)
        {
            Name = name;
            _states = states;
        }

        public void Write(IWriteGraph writeGraph)
        {
            writeGraph.StartObject(Name);

            foreach (IState state in _states)
                state.Write(writeGraph);

            writeGraph.EndObject();
        }

        public void Read(IReadGraph readGraph)
        {
            readGraph.StartObject(Name);

            foreach (IState state in _states)
                state.Read(readGraph);

            readGraph.EndObject();
        }
    }
}
