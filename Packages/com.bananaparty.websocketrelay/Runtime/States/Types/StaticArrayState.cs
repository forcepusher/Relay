using System;
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

        public void WriteState(IStateOutput stateOutput) => stateOutput.WriteStaticArray(StateName, ToStateList());

        public void ReadState(IStateInput stateInput) => stateInput.ReadStaticArray(StateName, ToStateList());

        public void CopyFrom(IState other)
        {
            if (other is StaticArrayState<T> otherState)
            {
                for (int i = 0; i < Math.Min(_states.Count, otherState._states.Count); i++)
                {
                    _states[i].CopyFrom(otherState._states[i]);
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
