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
        }

        public void Awake()
        {

        }
    }
}
