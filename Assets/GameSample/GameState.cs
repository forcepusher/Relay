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

        private IntegerValueState _playTimeState = new(nameof(_playTimeState), 0);

        private List<Item> _items = new List<Item>();
        private DynamicArrayState<Item> _itemsState;

        private void Awake()
        {
            _itemSpawnsState = new(nameof(_itemSpawns), _itemSpawns);
            _itemsState = new(nameof(_itemsState), _items);

            JsonWriteGraph jsonWriteGraph = new();
            Write(jsonWriteGraph);
            Debug.Log(jsonWriteGraph.ToString());
        }

        public string Name => transform.name;

        public void Write(IWriteGraph writeGraph)
        {
            writeGraph.StartObject(Name);

            _playTimeState.Write(writeGraph);
            _playerCharacterState.Write(writeGraph);
            _botCharacterState.Write(writeGraph);
            _itemSpawnsState.Write(writeGraph);
            _itemsState.Write(writeGraph);

            writeGraph.EndObject();
        }

        public void Read(IReadGraph readGraph)
        {
            readGraph.StartObject(Name);

            _playTimeState.Read(readGraph);
            _playerCharacterState.Read(readGraph);
            _botCharacterState.Read(readGraph);
            _itemSpawnsState.Read(readGraph);
            _itemsState.Read(readGraph);

            readGraph.EndObject();
        }
    }
}
