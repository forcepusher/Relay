using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class ItemSpawn : MonoBehaviour, IState
    {
        private const float RespawnDelay = 10f;
        private FloatState _timeToSpawn = new(nameof(_timeToSpawn), RespawnDelay);
        private List<IState> _states;

        public string StateName => transform.name;

        private void Awake()
        {
            _states = new List<IState> { _timeToSpawn };
        }

        public void WriteState(IStateOutput stateOutput) => stateOutput.WriteObject(StateName, _states);

        public void ReadState(IStateInput stateInput) => stateInput.ReadObject(StateName, _states);
    }
}
