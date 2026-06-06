using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class GameState : MonoBehaviour, IObjectNode
    {
        [SerializeField]
        private Character _playerCharacter = new();

        [SerializeField]
        private Character _botCharacter = new();

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
        }

        public void Awake()
        {

        }
    }
}
