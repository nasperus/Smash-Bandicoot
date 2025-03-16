using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovementScript : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed;
        
        [Header("Rotation")]
        [SerializeField] private float rotationSpeed;
        
        [Header("Jump")]
        [SerializeField] private float jumpForce;
        [SerializeField] private float jumpUpGravity;
        [SerializeField] private float airMovementSpeed;
       
        [Header("Spin")]
        [SerializeField] private float spinCooldown;
        [SerializeField] private float jumpSpinGravity;
        
        [Header("GroundChecks")]
        [SerializeField] private float groundDistance;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private GameObject groundCheck;
        
        private PlayerAnimationScript _playerAnimation;
        private Vector2 _playerInput;
        private Rigidbody _rb;
        
        private float _spinCd;
        private float _originalMovementSpeed;
        
        private int _doubleJumpCounter;
        private int _currentJumps;
        
        private const int MaxJumps = 2;
        private const int X = 0;
        private const int Z = 0;
        private const int Y = 0;
        private const int Zero = 0;
        
        private bool _canSpin;
        private bool _isJumping;
        private bool _isGrounded;
        public static bool IsRunning { get; private set; }
      
        
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
            SpinToAirGravity();
        }
        
        private void OnMove(InputValue value) {_playerInput = value.Get<Vector2>();}

        private void OnFire()
        {
            _canSpin = true;
            if (!(_spinCd <= Zero)) return;
            _playerAnimation.SpinAnimation();
            _spinCd = spinCooldown;
        }
        
        private void OnJump()
        {
            if (_isGrounded)
            {
                _currentJumps = _doubleJumpCounter;
                _canSpin = false;
            }
            if (_canSpin) return;
            
            if (_currentJumps >= MaxJumps) return;
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, jumpForce, _rb.linearVelocity.y);
            _playerAnimation.JumpAnimation();
            _currentJumps++;
        }
        private  void SpinCooldownTimer()
        {
            if (!(_spinCd >= Zero)) return;
            _spinCd -= Time.deltaTime;   
        }

        private void SpinToAirGravity()
        {
            if (_canSpin && !_isGrounded)
            {
                _rb.AddForce(new Vector3(X, jumpSpinGravity, Z), ForceMode.Acceleration);
            }
        }
        
        private void JumpGravity()
        {
            if (_isGrounded) return;
            if (!(_rb.linearVelocity.y > Zero)) return;
            _rb.AddForce(new Vector3(X, jumpUpGravity, Z), ForceMode.Acceleration);
        }
        
        
        private void DetectGround()
        {
            _isGrounded = Physics.Raycast(groundCheck.transform.position, Vector3.down, groundDistance, groundLayer);
        }
        
        private void PlayerMovement()
        {
            var movement = new Vector3(_playerInput.x, Y, _playerInput.y);
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

   
