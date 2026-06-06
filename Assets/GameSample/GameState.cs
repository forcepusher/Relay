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

        private IntegerObjectNode _playTimeNode = new(nameof(_playTimeNode), 0);
        private ObjectNode _playerCharacterNode;
        private ObjectNode _botCharacterNode;

        private IObjectNode _objectNode;

        public void Awake()
        {
            _playerCharacterNode = new ObjectNode(nameof(_playerCharacter), _playerCharacter.GetNodes());
            _botCharacterNode = new ObjectNode(nameof(_botCharacter), _botCharacter.GetNodes());

            _objectNode = new ObjectNode("GameState",
                _playTimeNode,
                _playerCharacterNode,
                _botCharacterNode
            );


            //string sampleJson = 

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
