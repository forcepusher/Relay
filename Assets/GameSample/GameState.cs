using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class GameState : MonoBehaviour
    {
        [SerializeField]
        private List<Character> _initialCharacters = new();

        private IntegerObjectNode _playTime = new(nameof(_playTime), 0);
        //private int _playTime = 0;

        ArrayObjectNode<Character> _characters;
        //List<ItemSpawn> _itemPickups = new();

        private IObjectNode _objectNode;
        private readonly IStateNode _stateStream = new JsonStateStream();
        public void Awake()
        {
            _characters = new ArrayObjectNode<Character>(nameof(_characters), _initialCharacters);

            _objectNode = new ObjectNode("GameState",
                _playTime,
                _characters
            );

            Debug.Log(_objectNode.OutputNameAndValue());
        }

        public void OnSaveButtonClick()
        {
            _objectNode.Serialize(_stateStream);
        }

        public void OnLoadButtonClick()
        {
            _objectNode.Deserialize(_stateStream);
        }
    }
}
