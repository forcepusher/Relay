using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class GameState : MonoBehaviour, IState
    {
        [SerializeField]
        private Character _playerCharacter;

        [SerializeField]
        private Character _botCharacter;

        [SerializeField]
        private List<ItemSpawn> _itemSpawns;

        [SerializeField]
        private List<Item> _items = new();

        [SerializeField]
        private Item _itemPrefab;

        private IntegerValueState _playTime = new(nameof(_playTime), 0);

        private StaticArrayState<ItemSpawn> _itemSpawnsState;
        private DynamicArrayState<Item> _itemsState;
        private int _nextItemId;

        private void Awake()
        {
            _itemSpawnsState = new(nameof(_itemSpawns), _itemSpawns);
            _itemsState = new(nameof(_items), _items);

            JsonWriteGraph jsonWriteGraph = new();
            Write(jsonWriteGraph);
            Debug.Log(jsonWriteGraph.ToString());
        }

        public string Name => transform.name;

        public void Write(IWriteGraph writeGraph)
        {
            writeGraph.StartObject(Name);

            _playTime.Write(writeGraph);
            _playerCharacter.Write(writeGraph);
            _botCharacter.Write(writeGraph);
            _itemSpawnsState.Write(writeGraph);
            _itemsState.Write(writeGraph);

            writeGraph.EndObject();
        }

        public void Read(IReadGraph readGraph)
        {
            readGraph.StartObject(Name);

            _playTime.Read(readGraph);
            _playerCharacter.Read(readGraph);
            _botCharacter.Read(readGraph);
            _itemSpawnsState.Read(readGraph);

            int itemCount = _itemsState.BeginRead(readGraph);
            var desiredItems = new List<Item>(itemCount);
            for (int i = 0; i < itemCount; i++)
            {
                Item item = Instantiate(_itemPrefab);
                item.Read(readGraph);
                desiredItems.Add(item);
            }
            _itemsState.FinishRead(readGraph, desiredItems);

            foreach (Item item in _itemsState.GetEntriesToDelete())
            {
                _items.Remove(item);
                Destroy(item.gameObject);
            }

            foreach (Item item in _itemsState.GetEntriesToAdd())
                _items.Add(item);

            foreach ((Item current, Item desired) in _itemsState.GetEntriesToWrite())
            {
                current.ApplyStateFrom(desired);
                Destroy(desired.gameObject);
            }

            readGraph.EndObject();
        }
    }
}
