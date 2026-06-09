using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BananaParty.WebSocketRelay.Samples
{
    [RequireComponent(typeof(CharacterController))]
    public class Character : MonoBehaviour, IState
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private float jumpHeight = 2f;

        private CharacterController controller;
        private float verticalVelocity;

        private FloatValueState _health = new(nameof(_health), 100f);
        private Vector3ValueState _position = new(nameof(_position), Vector3.zero);
        private List<IState> _states;

        public string StateName => transform.name;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            _states = new List<IState> { _health, _position };
        }

        private void Update()
        {
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

                if (controller.isGrounded && Keyboard.current.spaceKey.wasPressedThisFrame)
                {
                    verticalVelocity = Mathf.Sqrt(jumpHeight * 2f * 9.81f);
                }
            }

            Vector3 moveDirection = new Vector3(input.x, 0, input.y).normalized;

            if (moveDirection != Vector3.zero)
            {
                controller.Move(moveDirection * moveSpeed * Time.deltaTime);

                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            if (controller.isGrounded && verticalVelocity < 0)
            {
                verticalVelocity = -2f;
            }
            else
            {
                verticalVelocity -= 9.81f * Time.deltaTime;
            }

            controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);
        }
    }
}
