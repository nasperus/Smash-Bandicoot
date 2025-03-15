using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Player
{
    public class PlayerMovementScript : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float jumpForce;
        [SerializeField] private float groundDistance;
        [SerializeField] private float jumpUpGravity;
        [SerializeField] private float jumpDownGravity;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private GameObject groundCheck;

        
        private bool _isGrounded;
        private PlayerAnimationScript _playerAnimation;
        private Vector2 _playerInput;
        private Rigidbody _rb;
        private const int GroundGravity = 1;
        public static bool IsRunning { get; private set; }
        private bool _isJumping;

        private int _maxJumps = 2;
        private int _currentJums = 0;
        
        private void Start()
        {
            _rb = PlayerPhysicsScript.Instance.Rb;
            _rb.freezeRotation = true;
            _playerAnimation = GetComponent<PlayerAnimationScript>();
            
        }
        
        private void FixedUpdate()
        {
            PlayerMovement();
            DetectGround();
            JumpGravity();
        }
           
        
        private void OnMove(InputValue value) {_playerInput = value.Get<Vector2>();}
        
        private void OnFire() {_playerAnimation.SpinAnimation();}

        private void OnJump()
        {
            if (!_isGrounded) return;
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, jumpForce, _rb.linearVelocity.y);
            _playerAnimation.JumpAnimation();

        }
        
        private void JumpGravity()
        {
            if (!_isGrounded)
            {
                _rb.linearDamping = _rb.linearVelocity.y > 0 ? jumpUpGravity : jumpDownGravity;
            }
            else
            {
                _rb.linearDamping = GroundGravity;
            }
        }
        
        private void DetectGround()
        {
            _isGrounded = Physics.Raycast(groundCheck.transform.position, Vector3.down, groundDistance, groundLayer);
        }
        
        private void PlayerMovement()
        {
            var movement = new Vector3(_playerInput.x, 0, _playerInput.y);
            IsRunning = movement != Vector3.zero;
            //_rb.linearVelocity = movement * moveSpeed;
            _rb.linearVelocity = new Vector3(movement.x * moveSpeed, _rb.linearVelocity.y, movement.z * moveSpeed);
            if (!IsRunning) return;
            var targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _isGrounded ? Color.green : Color.red;
            Gizmos.DrawRay(groundCheck.transform.position, Vector3.down * groundDistance);
        }
    }
}

   
