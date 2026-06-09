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

            JsonStateOutput jsonStateOutput = new();
            Write(jsonStateOutput);
            Debug.Log(jsonStateOutput.ToString());
        }

        public string Name => transform.name;

        public void Write(IStateOutput writeGraph)
        {
            writeGraph.WriteObject(Name, _states);
        }

        public void Read(IStateInput readGraph)
        {
            readGraph.ReadObject(Name, _states);
        }
    }
}
