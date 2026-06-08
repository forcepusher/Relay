using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class GameState : MonoBehaviour, IStateNode
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

        private IntegerValueNode _playTime = new(nameof(_playTime), 0);

        private StaticArrayNode<ItemSpawn> _itemSpawnsNode;
        private DynamicArrayNode<Item> _itemsNode;
        private int _nextItemId;

        private void Awake()
        {
            _itemSpawnsNode = new(nameof(_itemSpawns), _itemSpawns);
            _itemsNode = new(nameof(_items), _items);

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
            _itemSpawnsNode.Write(writeGraph);
            _itemsNode.Write(writeGraph);

            writeGraph.EndObject();
        }

        public void Read(IReadGraph readGraph)
        {
            readGraph.StartObject(Name);

            _playTime.Read(readGraph);
            _playerCharacter.Read(readGraph);
            _botCharacter.Read(readGraph);
            _itemSpawnsNode.Read(readGraph);
            _itemsNode.Read(readGraph, CreateItem);
            ApplyItemReconcile();

            readGraph.EndObject();
        }

        private Item CreateItem()
        {
            Item item = Instantiate(_itemPrefab);
            item.name = $"Item_{_nextItemId++}";
            return item;
        }

        private void ApplyItemReconcile()
        {
            foreach (Item item in _itemsNode.GetEntriesToDelete())
            {
                _items.Remove(item);
                Destroy(item.gameObject);
            }

            foreach (Item item in _itemsNode.GetEntriesToAdd())
                _items.Add(item);

            foreach ((Item current, Item desired) in _itemsNode.GetEntriesToWrite())
            {
                current.ApplyStateFrom(desired);
                Destroy(desired.gameObject);
            }
        }
    }
}
