using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class GameState : MonoBehaviour, ISerializableState
    {
        private State<int> _playTimeState = new State<int>(0);
        //private int _playTime = 0;

        List<Character> _characters = new();
        //List<ItemSpawn> _itemPickups = new();

        readonly StateGraphNode _stateGraph = new();

        public void BuildStateGraph(StateGraphNode parent)
        {
            var stateNode = new StateGraphNode();
            stateNode.AddState(_playTimeState);
            parent.AddChildStateGraphNode(stateNode);
        }

        public void OnSaveButtonClick()
        {
            //_stateGraph.Save();
        }

        public void OnLoadButtonClick()
        {

        }
    }
}
