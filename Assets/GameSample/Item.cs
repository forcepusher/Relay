using System;
using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class Item : MonoBehaviour, IKeyedState
    {
        public GuidState Key = new(nameof(Key), Guid.Empty);
        public FloatState TimeToDisappear = new(nameof(TimeToDisappear), 5f);
        private List<IState> _states;

        public string StateName => transform.name;

        Guid IKeyedState.Key => Key.Value;

        private void Awake()
        {
            _states = new List<IState> { Key, TimeToDisappear };
        }

        public void WriteState(IStateOutput stateOutput) => stateOutput.WriteObject(StateName, _states);

        public void ReadState(IStateInput stateInput) => stateInput.ReadObject(StateName, _states);
    }
}
