using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class ItemSpawn : MonoBehaviour, IJsonState
    {
        private const float RespawnDelay = 10f;
        private FloatValueNode _timeToSpawn = new(nameof(_timeToSpawn), RespawnDelay);

        public string Name => transform.name;

        public void WriteToJson(JsonWriteGraph jsonWriteGraph)
        {
            jsonWriteGraph.StartObject(Name);
            _timeToSpawn.WriteStateToJson(jsonWriteGraph);
            jsonWriteGraph.EndObject();
        }

        public void ReadFromJson(JsonReadGraph jsonReadGraph)
        {
            jsonReadGraph.StartObject(Name);
            _timeToSpawn.ReadStateFromJson(jsonReadGraph);
            jsonReadGraph.EndObject();
        }
    }
}
