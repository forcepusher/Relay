using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class GameState : MonoBehaviour, IStateGraphObject
    {
        private IntegerState _playTimeState = new(0, nameof(_playTimeState));
        //private int _playTime = 0;

        List<Character> _characters = new();
        //List<ItemSpawn> _itemPickups = new();

        private readonly IStateGraphNode _objectGraphNode = new ObjectGraphNode();
        private readonly IStateStream _stateStream = new BinaryStateStream();

        public void BuildStateGraph(IStateGraphNode parent)
        {
            var stateNode = new ObjectGraphNode();
            stateNode.AddState(_playTimeState);
            parent.AddObject(stateNode);
        }

        public void OnSaveButtonClick()
        {
            _objectGraphNode.Serialize(_stateStream);
        }

        public void OnLoadButtonClick()
        {
            _objectGraphNode.Deserialize(_stateStream);
        }
    }
}
