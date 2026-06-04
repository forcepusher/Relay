using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

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
            foreach (var character in _characters)
                character.Serialize(_stateGraph);

            foreach (var itemPickup in _itemPickups)
                itemPickup.Serialize(_stateGraph);
        }

        public void Deserialize(StateGraph _stateGraph)
        {
            foreach (var character in _characters)
                character.Deserialize(_stateGraph);

            foreach (var itemPickup in _itemPickups)
                itemPickup.Deserialize(_stateGraph);
        }
    }
}
