using System;
using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class GameState : MonoBehaviour, ISerializableState
    {
        private State<int> _playTime = new State<int>(0);
        //private int _playTime = 0;

        List<Character> _characters = new();
        //List<ItemSpawn> _itemPickups = new();

        readonly StateGraphNode _stateGraph = new();

        public void OnSerializeButtonClick()
        {
            //Serialize(_stateGraph);
        }

        public void OnDeserializeButtonClick()
        {
            //Deserialize(_stateGraph);
        }

        public void BuildStateGraph(StateGraphNode parent)
        {
            var stateNode = new StateGraphNode();

            // Add state objects here
            stateNode.AddChild


            parent.AddChildStateGraphNode(stateNode);
        }

        public void Serialize(StateGraphNode _stateGraph)
        {
            // _stateGraph.WriteObject(nameof(_playTime), _playTime);

            // _stateGraph.WriteObject(nameof(_characters), _characters);
        }

        public void Deserialize(StateGraphNode _stateGraph)
        {
            //_playTime = (int)_stateGraph.ReadState(nameof(_playTime));
        }
    }
}
