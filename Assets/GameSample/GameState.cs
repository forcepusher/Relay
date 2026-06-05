using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class GameState : MonoBehaviour, ISerializableState
    {
        private IntegerState _playTimeState = new(0, nameof(_playTimeState));
        //private int _playTime = 0;

        List<Character> _characters = new();
        //List<ItemSpawn> _itemPickups = new();

        private readonly ObjectGraphNode _stateGraph = new();
        private readonly IStateStream _stateStream = new BinaryStateStream();

        public void BuildStateGraph(ObjectGraphNode parent)
        {
            var stateNode = new ObjectGraphNode();
            stateNode.AddState(_playTimeState);
            parent.AddChildStateGraphNode(stateNode);
        }

        public void OnSaveButtonClick()
        {
            _stateGraph.Serialize(_stateStream);
        }

        public void OnLoadButtonClick()
        {
            _stateGraph.Deserialize(_stateStream);
        }
    }
}
