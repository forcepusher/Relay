using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class Game : MonoBehaviour, IState
    {
        [SerializeField]
        private Character _playerCharacterState;

        [SerializeField]
        private Character _botCharacterState;

        [SerializeField]
        private List<ItemSpawn> _itemSpawns;
        private StaticArrayState<ItemSpawn> _itemSpawnsState;

        private IntegerState _playTimeState = new(nameof(_playTimeState), 0);

        [SerializeField]
        private Item _itemPrefab;

        private List<Item> _items = new List<Item>();
        private DynamicArrayState<Item> _itemsState;
        private Journal<Item> _itemsJournal;

        private List<IState> _states;

        private void Awake()
        {
            _itemSpawnsState = new(nameof(_itemSpawns), _itemSpawns);
            _itemsState = new(nameof(_itemsState), _items, new ItemLifecycle(this));

            _states = new List<IState>
            {
                _playTimeState,
                _playerCharacterState,
                _botCharacterState,
                _itemSpawnsState,
                _itemsState
            };

            _itemsJournal = new Journal<Item>(_items);
            _itemsJournal.Snapshot();

            JsonStateOutput jsonStateOutput = new();
            WriteState(jsonStateOutput);
            Debug.Log(jsonStateOutput.ToString());
        }

        public string StateName => transform.name;

        public void WriteState(IStateOutput writeGraph)
        {
            writeGraph.WriteObject(StateName, _states);
        }

        public void ReadState(IStateInput readGraph)
        {
            readGraph.ReadObject(StateName, _states);

            _itemsJournal.Snapshot();
        }

        private sealed class ItemLifecycle : IDynamicArrayLifecycle<Item>
        {
            private readonly Game _game;

            public ItemLifecycle(Game game) => _game = game;

            public Item Create() => Object.Instantiate(_game._itemPrefab, _game.transform);

            public void Delete(Item item) => Object.Destroy(item.gameObject);
        }
    }
}
