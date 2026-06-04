using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class GameState : MonoBehaviour, ISerializableState
    {
        List<Character> _characters = new();
        List<ItemSpawn> _itemPickups = new();

        readonly StateGraph _stateGraph = new();

        public void OnSerializeButtonClick()
        {
            Serialize(_stateGraph);
        }

        public void OnDeserializeButtonClick()
        {
            Deserialize(_stateGraph);
        }

        public void Serialize(StateGraph _stateGraph)
        {
            _stateGraph.WriteState("Count", _characters.Count);
            foreach (var character in _characters)
            {
                character.Serialize(_stateGraph);
            }

            _stateGraph.WriteState("Count", _itemPickups.Count);
            foreach (var itemPickup in _itemPickups)
            {
                itemPickup.Serialize(_stateGraph);
            }
        }

        public void Deserialize(StateGraph _stateGraph)
        {
            KeyedState characterKeyStatePair = _stateGraph.ReadState();
            int characterCount = (int)characterKeyStatePair.State;
            for (int i = 0; i < characterCount; i++)
            {
                var character = new Character();
                character.Deserialize(_stateGraph);
                _characters.Add(character);
            }

            KeyedState itemPickupKeyStatePair = _stateGraph.ReadState();
            int itemPickupCount = (int)itemPickupKeyStatePair.State;
            for (int i = 0; i < itemPickupCount; i++)
            {
                var itemPickup = new ItemSpawn();
                itemPickup.Deserialize(_stateGraph);
                _itemPickups.Add(itemPickup);
            }
        }
    }
}
