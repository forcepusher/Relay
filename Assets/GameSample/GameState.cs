using System;
using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class GameState : MonoBehaviour, ISerializableState
    {
        private int _playTime = 0;

        List<Character> _characters = new();
        //List<ItemSpawn> _itemPickups = new();

        readonly StateNode _stateGraph = new();

        public void OnSerializeButtonClick()
        {
            //Serialize(_stateGraph);
        }

        public void OnDeserializeButtonClick()
        {
            //Deserialize(_stateGraph);
        }

        public void BuildStateGraph(StateNode parent)
        {
            var stateNode = new StateNode();
            //stateNode
            parent.AddChild();
        }

        public void Serialize(StateNode _stateGraph)
        {
            // _stateGraph.WriteObject(nameof(_playTime), _playTime);

            // _stateGraph.WriteObject(nameof(_characters), _characters);
        }

        public void Deserialize(StateNode _stateGraph)
        {
            //_playTime = (int)_stateGraph.ReadState(nameof(_playTime));
        }
    }
}
