using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class GameState : MonoBehaviour, IObjectNode, IJsonState
    {
        [SerializeField]
        private Character _playerCharacter;

        [SerializeField]
        private Character _botCharacter;

        [SerializeField]
        private List<ItemSpawn> _itemSpawns;

        private IntegerValueNode _playTime = new(nameof(_playTime), 0);

        private ArrayNode<ItemSpawn> _itemSpawnsNode;

        private void Awake()
        {
            _itemSpawnsNode = new(nameof(_itemSpawns), _itemSpawns);
        }

        public string Name => transform.name;
        public List<INode> GetNodes()
        {
            return new List<INode>
            {
                _playTime,
                _playerCharacter,
                _botCharacter,
                _itemSpawnsNode
            };
        }

        public void WriteStateToJson(JsonWriteStateGraph jsonStateGraph)
        {
            jsonStateGraph.StartObject(Name);

            foreach (INode node in GetNodes())
                node.WriteStateToJson(jsonStateGraph);

            jsonStateGraph.EndObject();
        }

        public void ReadStateFromJson(JsonReadStateGraph jsonReadStateGraph)
        {
            jsonReadStateGraph.StartObject(Name);

            foreach (INode node in GetNodes())
                node.ReadStateFromJson(jsonReadStateGraph);

            jsonReadStateGraph.EndObject();
        }

        public void Awake()
        {
            JsonWriteStateGraph _jsonStateGraph = new();
            WriteStateToJson(_jsonStateGraph);
            Debug.Log(_jsonStateGraph.ToString());


            string sampleJson = @"{
             ""GameState"": {
               ""_playTime"": 0,
               ""PlayerCharacter"": {
                 ""_health"": 100.0,
                 ""_position"": { ""x"": 0.0, ""y"": 0.0, ""z"": 0.0 }
               },
               ""BotCharacter"": {
                 ""_health"": 100.0,
                 ""_position"": { ""x"": 0.0, ""y"": 0.0, ""z"": 0.0 }
               }
             }
            }";

            //INode parsedSampleJsonRoot = Json.Parse(sampleJson);

            //Debug.Log(Json.Serialize(parsedSampleJsonRoot));
        }
    }
}
