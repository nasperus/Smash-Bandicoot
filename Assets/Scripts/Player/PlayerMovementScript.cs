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
        [SerializeField] private float spinTimer;
        [SerializeField] private float airMovementSpeed;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private GameObject groundCheck;

        private float _originalMovementSpeed;
        private bool _isGrounded;
        private PlayerAnimationScript _playerAnimation;
        private Vector2 _playerInput;
        private Rigidbody _rb;
        private const int GroundGravity = 0;
        public static bool IsRunning { get; private set; }
        private bool _isJumping;
        private const int MaxJumps = 2;
        private int _currentJumps;
        private float _spinCd;
      
        
        private void Start()
        {
            _rb = PlayerPhysicsScript.Instance.Rb;
            _rb.freezeRotation = true;
            _playerAnimation = GetComponent<PlayerAnimationScript>();
            _originalMovementSpeed = moveSpeed;
        }
        
        private void FixedUpdate()
        {
            PlayerMovement();
            DetectGround();
            JumpGravity();
            SpinCooldownTimer();
        }
        
        private void OnMove(InputValue value) {_playerInput = value.Get<Vector2>();}

        private void OnFire()
        {
            if (!(_spinCd <= 0)) return;
            _playerAnimation.SpinAnimation();
            _spinCd = spinTimer;
        }

        private  void SpinCooldownTimer()
        {
            if (!(_spinCd >= 0)) return;
            _spinCd -= Time.deltaTime;   
        }

        private void OnJump()
        {
            if (_isGrounded)
            {
                _currentJumps = 0;
            }
            if (_currentJumps < MaxJumps)
            {
                _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, jumpForce, _rb.linearVelocity.y);
                _playerAnimation.JumpAnimation();
                _currentJumps++;
            }
        }
        
        private void JumpGravity()
        {
            if (!_isGrounded)
            {
                _rb.linearDamping = _rb.linearVelocity.y > 0 ? jumpUpGravity : jumpDownGravity;
            }else
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
            
            moveSpeed = _isGrounded ? _originalMovementSpeed : _originalMovementSpeed - airMovementSpeed;
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

   
