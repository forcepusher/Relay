using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class ItemSpawn : MonoBehaviour, IState
    {
        private const float RespawnDelay = 3f;

        [SerializeField]
        private Item _itemPrefab;

        private FloatState _timeToSpawn = new(nameof(_timeToSpawn), RespawnDelay);
        private List<IState> _states;

        public string StateName => transform.name;

        private void Awake()
        {
            _states = new List<IState> { _timeToSpawn };
        }

        private void Update()
        {
            if (_timeToSpawn.Value > 0f)
                _timeToSpawn.Value -= Time.deltaTime;
            else
                Instantiate(_itemPrefab, transform);
        }

        public void WriteState(IStateOutput stateOutput) => stateOutput.WriteObject(StateName, _states);

        public void ReadState(IStateInput stateInput) => stateInput.ReadObject(StateName, _states);
    }
}
