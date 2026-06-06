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

        public void WriteState(IStateGraph<string> stateGraph)
        {
            stateGraph.StartChildGroup(Name);
            _playTime.WriteState<string>(stateGraph);

            foreach (INode node in GetNodes())
                node.WriteState<string>(stateGraph);

            stateGraph.EndChildGroup();
        }

        public void ReadState(IStateGraph<string> stateGraph)
        {

        }

        public void Awake()
        {
            JsonDataGraph _jsonDataGraph = new();
            WriteState(_jsonDataGraph);


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
