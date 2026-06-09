using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class GameState : MonoBehaviour, IState
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
            _itemsState = new(nameof(_itemsState), _items);

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

            foreach (Item item in _itemsJournal.GetDeleteEntries())
                Destroy(item.gameObject);

            foreach (Item item in _itemsJournal.GetNewEntries())
            {
                Item instance = Instantiate(_itemPrefab, transform);
                instance.TimeToDisappear = item.TimeToDisappear;
                _items[_items.IndexOf(item)] = instance;
                Destroy(item.gameObject);
            }

            foreach (Item item in _itemsJournal.GetUpdatedEntries())
                item.gameObject.SetActive(true);

            _itemsJournal.Snapshot();
        }
    }
}
