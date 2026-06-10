using System;
using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class DynamicArrayState<T> : IState where T : IKeyedState
    {
        public string StateName { get; }
        private readonly List<T> _states;
        private readonly IFactory<T> _factory;

        public DynamicArrayState(string name, List<T> states)
        {
            StateName = name;
            _states = states;
        }

        public DynamicArrayState(string name, List<T> states, IFactory<T> factory)
        {
            StateName = name;
            _states = states;
            _factory = factory;
        }

        public void WriteState(IStateOutput stateOutput) => stateOutput.WriteDynamicArray(StateName, ToStateList());

        public void ReadState(IStateInput stateInput)
        {
            if (_factory != null)
                stateInput.ReadDynamicArray(StateName, _states, _factory);
            else
                stateInput.ReadDynamicArray(StateName, _states);
        }

        public void CopyFrom(IState other)
        {
            if (other is DynamicArrayState<T> otherState)
            {
                for (int i = 0; i < Math.Min(_states.Count, otherState._states.Count); i++)
                {
                    StateBridge.Copy(otherState._states[i], _states[i]);
                }
            }
        }

        private List<IState> ToStateList()
        {
            var states = new List<IState>(_states.Count);
            foreach (T state in _states)
                states.Add(state);

            return states;
        }
    }
}
