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

        public void Write(IDataGraph<string> dataGraph)
        {
            dataGraph.StartChildGroup(Name);
            _playTime.WriteToGraph(dataGraph);

            foreach (INode node in GetNodes())
                node.WriteToGraph(dataGraph);

            dataGraph.EndChildGroup();
        }
        public void Read(IDataGraph<string> dataGraph)
        {

        }

        public void Awake()
        {
            JsonDataGraph _jsonDataGraph = new();
            Write(_jsonDataGraph);


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
