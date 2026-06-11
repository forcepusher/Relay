using UnityEngine;
using UnityEngine.InputSystem;

namespace BananaParty.WebSocketRelay.Samples
{
    public class BotCharacterInput : MonoBehaviour, IICharacterInput
    {
        public Vector2 MovementInput { get; private set; } = Vector2.zero;

        public bool JumpInput { get; private set; } = false;

        public void PollInput()
        {

        }
    }
}
