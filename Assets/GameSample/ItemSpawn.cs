using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BananaParty.WebSocketRelay.Samples
{
    public class ItemSpawn : MonoBehaviour, IObjectNode, IJsonState
    {
        private const float RespawnDelay = 10f;
        private FloatValueNode _timeToSpawn = new(nameof(_timeToSpawn), RespawnDelay);

        public string Name => transform.name;
        public List<INode> GetNodes()
        {
            return new List<INode>
            {
                _timeToSpawn
            };
        }

        public void WriteStateToJson(JsonWriteStateGraph jsonStateGraph)
        {
            jsonStateGraph.StartChildGroup(Name);
            _timeToSpawn.WriteStateToJson(jsonStateGraph);
            jsonStateGraph.EndChildGroup();
        }

        public void ReadStateFromJson(JsonReadStateGraph jsonReadStateGraph)
        {
            jsonReadStateGraph.StartChildGroup(Name);
            _timeToSpawn.ReadStateFromJson(jsonReadStateGraph);
            jsonReadStateGraph.EndChildGroup();
        }
    }
}
