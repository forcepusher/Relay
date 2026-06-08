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

        private IntegerValueState _playTime = new(nameof(_playTime), 0);

        private StaticArrayState<ItemSpawn> _itemSpawnsState;
        private DynamicArrayState<Item> _itemsState;

        private void Awake()
        {
            _itemSpawnsState = new(nameof(_itemSpawns), _itemSpawns);
            _itemsState = new(nameof(_itemsState), new List<Item>());

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
            _itemsState.Read(readGraph);

            readGraph.EndObject();
        }
    }
}
