using System;
using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class DynamicArrayState<T> : IState where T : IState
    {
        public string StateName { get; }
        private readonly List<T> _states;
        private readonly Func<T> _instantiate;
        private readonly Action<T> _delete;

        public DynamicArrayState(string name, List<T> states, Func<T> instantiate = null, Action<T> delete = null)
        {
            StateName = name;
            _states = states;
            _instantiate = instantiate;
            _delete = delete;
        }

        public void WriteState(IStateOutput stateOutput) => stateOutput.WriteDynamicArray(StateName, ToStateList());

        public void ReadState(IStateInput stateInput) =>
            stateInput.ReadDynamicArray(StateName, _states, _instantiate, _delete);

        private List<IState> ToStateList()
        {
            var states = new List<IState>(_states.Count);
            foreach (T state in _states)
                states.Add(state);

            return states;
        }
    }
}
