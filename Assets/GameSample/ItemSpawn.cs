using System;
using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class ItemSpawn : MonoBehaviour, IState, IFactory<Item>
    {
        private const float RespawnDelay = 3f;

        [SerializeField]
        private Item _itemPrefab;

        private FloatState _timeToSpawn = new(nameof(_timeToSpawn), RespawnDelay);
        private List<Item> _items = new List<Item>();
        private DynamicArrayState<Item> _itemsState;
        private List<IState> _states;

        public string StateName => transform.name;

        private void Awake()
        {
            _itemsState = new(nameof(_itemsState), _items, this);
            _states = new List<IState> { _timeToSpawn, _itemsState };
        }

        private void Update()
        {
            if (_timeToSpawn.Value > 0f)
                _timeToSpawn.Value -= Time.deltaTime;
            else
            {
                _items.Add(Create());
                _timeToSpawn.Value = RespawnDelay;
            }
        }

        public Item Create()
        {
            Item item = Instantiate(_itemPrefab, transform);
            if (item.Id == Guid.Empty)
                item.Id = Guid.NewGuid();

            return item;
        }

        public void Dispose(Item item) => Destroy(item.gameObject);

        public Guid GetKey(Item item) => item.Id;

        public void WriteState(IStateOutput stateOutput) => stateOutput.WriteObject(StateName, _states);

        public void ReadState(IStateInput stateInput) => stateInput.ReadObject(StateName, _states);
    }
}
