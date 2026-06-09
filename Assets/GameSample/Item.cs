using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class Item : MonoBehaviour, IState
    {
        private FloatState _timeToDisappear = new(nameof(_timeToDisappear), 5f);
        private List<IState> _states;

        public string StateName => transform.name;

        public float TimeToDisappear
        {
            get => _timeToDisappear.Value;
            set => _timeToDisappear.Value = value;
        }

        private void Awake()
        {
            _states = new List<IState> { _timeToDisappear };
        }

        public void WriteState(IStateOutput stateOutput) => stateOutput.WriteObject(StateName, _states);

        public void ReadState(IStateInput stateInput) => stateInput.ReadObject(StateName, _states);
    }
}
