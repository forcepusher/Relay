using System;
using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class GameState : MonoBehaviour, ISerializableState
    {
        List<Character> _characters = new();
        List<ItemSpawn> _itemPickups = new();

        readonly StateGraph _stateGraph = new();

        public void OnSerializeButtonClick()
        {
            Serialize(_stateGraph);
        }

        public void OnDeserializeButtonClick()
        {
            Deserialize(_stateGraph);
        }

        public void Serialize(StateGraph _stateGraph)
        {
            _stateGraph.WriteState("Count", _characters.Count);
            foreach (var character in _characters)
            {
                character.Serialize(_stateGraph);
            }

            _stateGraph.WriteState("Count", _itemPickups.Count);
            foreach (var itemPickup in _itemPickups)
            {
                itemPickup.Serialize(_stateGraph);
            }
        }

        public void Deserialize(StateGraph _stateGraph)
        {
            int characterCount = (int)_stateGraph.ReadState().State;
            Reconcile(_characters, characterCount, _stateGraph, () => new Character());

            int itemPickupCount = (int)_stateGraph.ReadState().State;
            Reconcile(_itemPickups, itemPickupCount, _stateGraph, () => new ItemSpawn());
        }

        private void Reconcile<T>(List<T> list, int count, StateGraph graph, Func<T> factory) where T : ISerializableState
        {
            for (int i = 0; i < Math.Min(list.Count, count); i++)
            {
                list[i].Deserialize(graph);
            }

            if (count > list.Count)
            {
                for (int i = list.Count; i < count; i++)
                {
                    T item = factory();
                    item.Deserialize(graph);
                    list.Add(item);
                }
            }
            else if (list.Count > count)
            {
                list.RemoveRange(count, list.Count - count);
            }
        }
    }
}
