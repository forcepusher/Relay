using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class GameState : MonoBehaviour, IObjectNode
    {
        [SerializeField]
        private Character _playerCharacter = new();

        [SerializeField]
        private Character _botCharacter = new();

        private IntegerValueNode _playTime = new(nameof(_playTime), 0);

        public string Name => transform.name;
        public List<INode> GetNodes()
        {
            return new List<INode>
            {
                _playTime,
                _playerCharacter,
                _botCharacter
            };
        }

        public void Awake()
        {
            //_playerCharacterNode = new ObjectNode(nameof(_playerCharacter), _playerCharacter.GetNodes());
            //_botCharacterNode = new ObjectNode(nameof(_botCharacter), _botCharacter.GetNodes());

            //_objectNode = new ObjectNode("GameState",
            //    _playTimeNode,
            //    _playerCharacterNode,
            //    _botCharacterNode
            //);


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
