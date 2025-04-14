using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovementScript : MonoBehaviour
    {
        private const int MaxJumps = 2;

        [Header("Dependencies")]
        [SerializeField] private PlayerAnimationScript playerAnimation;
        [SerializeField] private PlayerGroundCheckScript playerGroundCheck;
        [SerializeField] private PlayerPhysicsScript playerPhysics;

        [Header("Movement")] [SerializeField] private float moveSpeed;

        [Header("Rotation")] [SerializeField] private float rotationSpeed;

        [Header("Jump")] [SerializeField] private float jumpForce;

        [Header("Air Gravity/Movement")]
        [SerializeField] private float jumpGravity;
        [SerializeField] private float airMovementSpeed;
        
        private int _currentJumps;
        private int _doubleJumpCounter;
        private Vector2 _playerInput;
        private Rigidbody _rb;
        public bool CanSpin { get; set; }
        public static bool IsRunning { get; private set; }

        
        private void Start()
        {
            _rb = playerPhysics.GetRigidbody();
            _rb.freezeRotation = true;
        }

        private void FixedUpdate()
        {
            PlayerMovement();
            JumpGravity();
        }

        private void OnMove(InputValue value) { _playerInput = value.Get<Vector2>();}
        
        private void OnJump()
        {
            if (playerGroundCheck.IsGrounded)
            {
                _currentJumps = _doubleJumpCounter;
                CanSpin = false;
            }
            if (CanSpin) return;

            if (_currentJumps >= MaxJumps) return;
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, jumpForce, _rb.linearVelocity.y);
            playerAnimation.JumpAnimation();
            _currentJumps++;
        }

        private void JumpGravity()
        {
            if (playerGroundCheck.IsGrounded) return;
            
            if (_rb.linearVelocity.y <= 0) return;
            //playerAnimation.JumpAnimation();
            _rb.AddForce(new Vector3(0, jumpGravity, 0), ForceMode.Acceleration);
       
        }

        private void PlayerMovement()
        {
            var movement = new Vector3(_playerInput.x, 0, _playerInput.y);
            IsRunning = movement != Vector3.zero;

            var currentMoveSpeed = playerGroundCheck.IsGrounded ? moveSpeed : moveSpeed - airMovementSpeed;
            _rb.linearVelocity = new Vector3(movement.x * currentMoveSpeed, _rb.linearVelocity.y,
                movement.z * currentMoveSpeed);

            if (!IsRunning) return;
            var targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}