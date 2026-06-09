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

        public void Write(IStateOutput writeGraph)
        {
            writeGraph.StartObject(Name);

            foreach (IState state in _states)
                state.Write(writeGraph);

            writeGraph.EndObject();
        }

        public void Read(IStateInput readGraph)
        {
            readGraph.StartObject(Name);

            foreach (IState state in _states)
                state.Read(readGraph);

            readGraph.EndObject();
        }
    }
}
