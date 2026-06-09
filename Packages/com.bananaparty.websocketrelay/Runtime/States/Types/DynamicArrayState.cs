using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class DynamicArrayState<T> : IState where T : IState
    {
        public string StateName { get; }
        private readonly List<T> _states;
        private readonly IDynamicArrayFactory<T> _factory;

        public DynamicArrayState(string name, List<T> states, IDynamicArrayFactory<T> factory = null)
        {
            StateName = name;
            _states = states;
            _factory = factory;
        }

        public void WriteState(IStateOutput stateOutput) => stateOutput.WriteDynamicArray(StateName, ToStateList());

        public void ReadState(IStateInput stateInput) =>
            stateInput.ReadDynamicArray(StateName, _states, _factory);

        private List<IState> ToStateList()
        {
            var states = new List<IState>(_states.Count);
            foreach (T state in _states)
                states.Add(state);

            return states;
        }
    }
}
