using System;
using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class GameState : MonoBehaviour, ISerializableState
    {
        private int _playTime = 0;

        List<Character> _characters = new();
        List<ItemSpawn> _itemPickups = new();

        readonly StateGraph _stateGraph = new();

        public void OnSerializeButtonClick()
        {
            //Serialize(Key,_stateGraph);
        }

        public void OnDeserializeButtonClick()
        {
            //Deserialize(Key, _stateGraph);
        }

        public void Serialize(StateGraph _stateGraph)
        {
            _stateGraph.WriteState("PlayTime", _playTime);
            _stateGraph.WriteState("CharacterCount", _characters.Count);

            //_stateGraph.WriteState("Count", _characters.Count);
            //foreach (var character in _characters)
            //{
            //    character.Serialize(character.GetKey(), _stateGraph);
            //}

            //_stateGraph.WriteState("Count", _itemPickups.Count);
            //foreach (var itemPickup in _itemPickups)
            //{
            //    itemPickup.Serialize(itemPickup.GetKey(), _stateGraph);
            //}
        }

        public void Deserialize(StateGraph _stateGraph)
        {
            _playTime = (int)_stateGraph.ReadState().State;

            //int characterCount = (int)_stateGraph.ReadState().State;
            //Reconcile(_characters, characterCount, _stateGraph, () => new Character());

            //int itemPickupCount = (int)_stateGraph.ReadState().State;
            //Reconcile(_itemPickups, itemPickupCount, _stateGraph, () => new ItemSpawn());
        }
    }
}
