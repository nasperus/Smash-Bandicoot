using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovementScript : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;
        private PlayerAnimationScript _playerAnimation;
        private Vector2 _playerInput;
        private Rigidbody _rb;
        public static bool IsRunning { get; private set; }
        
        private void Start()
        {
            _rb = PlayerPhysicsScript.Instance.Rb;
            _rb.freezeRotation = true;
            _playerAnimation = GetComponent<PlayerAnimationScript>();
        }
        
        private void FixedUpdate()
        {
            PlayerMovement();
        }
        
        private void OnMove(InputValue value) {_playerInput = value.Get<Vector2>();}
        
        private void OnFire() {_playerAnimation.SpinAnimation();}
        
        private void OnJump() {}
        
        private void PlayerMovement()
        {
            var movement = new Vector3(_playerInput.x, 0, _playerInput.y);
            IsRunning = movement != Vector3.zero;
            if (!IsRunning) return;
            var targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            _rb.linearVelocity = movement * moveSpeed;
        }

    }
}

   
