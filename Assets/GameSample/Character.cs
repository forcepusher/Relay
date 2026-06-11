using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BananaParty.WebSocketRelay.Samples
{
    [RequireComponent(typeof(CharacterController))]
    public class Character : MonoBehaviour, IState, IICharacterInput
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private float jumpHeight = 2f;

        private CharacterController _characteController;
        private IICharacterInput _characterInput;

        private float _jumpVelocity;

        private FloatState _health = new(nameof(_health), 100f);
        private Vector3State _position = new(nameof(_position), Vector3.zero);
        private List<IState> _states;

        public string StateName => transform.name;

        private void Awake()
        {
            _characteController = GetComponent<CharacterController>();

            _states = new List<IState> { _health, _position };
        }

        private void Update()
        {
            _characterInput.PollInput();

            Move();
        }

        public void WriteState(IStateOutput stateOutput) => stateOutput.WriteObject(StateName, _states);

        public void ReadState(IStateInput stateInput) => stateInput.ReadObject(StateName, _states);

        private void Move()
        {
            Vector2 input = Vector2.zero;

            if (Keyboard.current != null)
            {
                if (Keyboard.current.wKey.isPressed) input.y += 1f;
                if (Keyboard.current.sKey.isPressed) input.y -= 1f;
                if (Keyboard.current.aKey.isPressed) input.x -= 1f;
                if (Keyboard.current.dKey.isPressed) input.x += 1f;

                if (_characteController.isGrounded && Keyboard.current.spaceKey.wasPressedThisFrame)
                {
                    _jumpVelocity = Mathf.Sqrt(jumpHeight * 2f * 9.81f);
                }
            }

            Vector3 moveDirection = new Vector3(input.x, 0, input.y).normalized;

            if (moveDirection != Vector3.zero)
            {
                _characteController.Move(moveDirection * moveSpeed * Time.deltaTime);

                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            if (_characteController.isGrounded && _jumpVelocity < 0)
            {
                _jumpVelocity = -2f;
            }
            else
            {
                _jumpVelocity -= 9.81f * Time.deltaTime;
            }

            _characteController.Move(Vector3.up * _jumpVelocity * Time.deltaTime);
        }
    }
}
