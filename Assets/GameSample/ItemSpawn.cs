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
        private List<Item> _items = new List<Item>();
        private DynamicArrayState<Item> _itemsState;
        private List<IState> _states;

        public string StateName => transform.name;

        private void Awake()
        {
            _itemsState = new DynamicArrayState<Item>(nameof(_itemsState), _items, new ItemLifecycle(this));
            _states = new List<IState> { _timeToSpawn, _itemsState };
        }

        private void Update()
        {
            if (_timeToSpawn.Value > 0f)
                _timeToSpawn.Value -= Time.deltaTime;
            else
            {
                _items.Add(Object.Instantiate(_itemPrefab, transform));
                _timeToSpawn.Value = RespawnDelay;
            }
        }

        public void WriteState(IStateOutput stateOutput)
        {
            stateOutput.WriteObject(StateName, _states);
        }

        public void ReadState(IStateInput stateInput)
        {
            stateInput.ReadObject(StateName, _states);
        }

        private sealed class ItemLifecycle : IDynamicArrayLifecycle<Item>
        {
            private readonly ItemSpawn _spawn;

            public ItemLifecycle(ItemSpawn spawn)
            {
                _spawn = spawn;
            }

            public Item Create()
            {
                return Object.Instantiate(_spawn._itemPrefab, _spawn.transform);
            }

            public void Delete(Item item)
            {
                Object.Destroy(item.gameObject);
            }
        }
    }
}
