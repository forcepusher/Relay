using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class GameState : MonoBehaviour
    {
        private IntegerObjectNode _playTimeState = new(nameof(_playTimeState), 0);
        //private int _playTime = 0;

        List<Character> _characters = new();
        //List<ItemSpawn> _itemPickups = new();

        private IObjectNode _objectNode;
        private readonly IStateStream _stateStream = new JsonStateStream();
        public void Awake()
        {
            _objectNode = new ObjectNode("GameState",
                _playTimeState
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
