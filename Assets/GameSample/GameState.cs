using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class GameState : MonoBehaviour, ISerializableState
    {
        List<Character> _characters = new();
        List<ItemPickup> _itemPickups = new();

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
            foreach (var character in _characters)
                _stateGraph.PushState(character);

            foreach (var itemPickup in _itemPickups)
                _stateGraph.PushState(itemPickup);
        }

        public void Deserialize(StateGraph _stateGraph)
        {
            _stateGraph.PopState(_characters);
            _stateGraph.PopState(_itemPickups);
        }
    }
}
