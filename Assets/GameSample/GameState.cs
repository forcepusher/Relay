using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class GameState : MonoBehaviour, IObjectNode, IState<string>
    {
        [SerializeField]
        private Character _playerCharacter;

        [SerializeField]
        private Character _botCharacter;

        private IntegerValueNode _playTime = new(nameof(_playTime), 0);

        public string Name => transform.name;
        public List<INode> GetNodes()
        {
            return new List<INode>
            {
                _playTime,
                _playerCharacter,
                _botCharacter
            };
        }

        public void WriteJsonState(JsonStateGraph jsonStateGraph)
        {
            jsonStateGraph.StartChildGroup(Name);
            _playTime.WriteJsonState(jsonStateGraph);

            foreach (INode node in GetNodes())
                node.WriteJsonState(jsonStateGraph);

            jsonStateGraph.EndChildGroup();
        }

        public void ReadJsonState(JsonStateGraph jsonStateGraph)
        {

        }

        public void Awake()
        {
            JsonStateGraph _jsonStateGraph = new();
            WriteJsonState(_jsonStateGraph);
            Debug.Log(_jsonStateGraph.ToString());

            //string sampleJson = @"{
            //  ""GameState"": {
            //    ""_playTime"": 0,
            //    ""PlayerCharacter"": {
            //      ""_health"": 100.0,
            //      ""_position"": { ""x"": 0.0, ""y"": 0.0, ""z"": 0.0 }
            //    },
            //    ""BotCharacter"": {
            //      ""_health"": 100.0,
            //      ""_position"": { ""x"": 0.0, ""y"": 0.0, ""z"": 0.0 }
            //    }
            //  }
            //}";

            //INode parsedSampleJsonRoot = Json.Parse(sampleJson);

            //Debug.Log(Json.Serialize(parsedSampleJsonRoot));
        }
    }
}
