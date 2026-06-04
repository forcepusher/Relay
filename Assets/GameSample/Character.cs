using UnityEngine;
using UnityEngine.InputSystem;

namespace BananaParty.WebSocketRelay.Samples
{
    [RequireComponent(typeof(CharacterController))]
    public class Character : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotationSpeed = 10f;

        private CharacterController controller;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector2 input = Vector2.zero;

            if (Keyboard.current != null)
            {
                if (Keyboard.current.wKey.isPressed) input.y += 1f;
                if (Keyboard.current.sKey.isPressed) input.y -= 1f;
                if (Keyboard.current.aKey.isPressed) input.x -= 1f;
                if (Keyboard.current.dKey.isPressed) input.x += 1f;
            }

            Vector3 moveDirection = new Vector3(input.x, 0, input.y).normalized;

            if (moveDirection != Vector3.zero)
            {
                // Move the character
                controller.Move(moveDirection * moveSpeed * Time.deltaTime);

                // Rotate to face movement direction
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            // Basic gravity
            if (!controller.isGrounded)
            {
                controller.Move(Vector3.down * 9.81f * Time.deltaTime);
            }
        }
    }
}
