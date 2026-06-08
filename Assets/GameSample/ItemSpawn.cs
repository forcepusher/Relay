using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BananaParty.WebSocketRelay.Samples
{
    public class ItemSpawn : MonoBehaviour, IState, IJsonState
    {
        private const float RespawnDelay = 10f;
        private FloatValueNode _timeToSpawn = new(nameof(_timeToSpawn), RespawnDelay);

        public string Name => transform.name;

        public void WriteStateToJson(JsonWriteGraph jsonStateGraph)
        {
            jsonStateGraph.StartObject(Name);
            _timeToSpawn.WriteStateToJson(jsonStateGraph);
            jsonStateGraph.EndObject();
        }

        public void ReadStateFromJson(JsonReadGraph jsonReadStateGraph)
        {
            jsonReadStateGraph.StartObject(Name);
            _timeToSpawn.ReadStateFromJson(jsonReadStateGraph);
            jsonReadStateGraph.EndObject();
        }
    }
}
