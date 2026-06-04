using BananaParty.WebSocketRelay.Samples;
using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class GameState : MonoBehaviour, ISerializableState
    {
        private CharacterList _characterList;

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
            _characterList.Serialize(_stateGraph);
        }

        public void Deserialize(StateGraph _stateGraph)
        {
            _characterList.Deserialize(_stateGraph);
        }
    }
}
