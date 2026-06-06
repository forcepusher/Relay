using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class GameState : MonoBehaviour
    {
        [SerializeField]
        private Character _playerCharacter = new();

        [SerializeField]
        private Character _botCharacter = new();

        private IntegerObjectNode _playTime = new(nameof(_playTime), 0);
        private ObjectNode _playerCharacterNode;
        //private int _playTime = 0;

        //ArrayObjectNode<Character> _characters;
        //List<ItemSpawn> _itemPickups = new();

        private IObjectNode _objectNode;

        public void Awake()
        {
            _playerCharacterNode = new ObjectNode(nameof(_playerCharacter), _playerCharacter.GetNodes());

            _objectNode = new ObjectNode("GameState",
                _playTime,
                _playerCharacterNode
            );


            string sampleJson = 

        }

        public void OnSaveButtonClick()
        {
            //_objectNode.Serialize(_stateStream);
        }

        public void OnLoadButtonClick()
        {
            //_objectNode.Deserialize(_stateStream);
        }
    }
}
