using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerSpinScript : MonoBehaviour
    {
        
        [Header("Dependencies")]
        [SerializeField] private PlayerAnimationScript playerAnimation;
        [SerializeField ]private PlayerGroundCheckScript playerGroundCheck;
        [SerializeField] private PlayerMovementScript playerMovement;
        
        [Header("Spin")]
        [SerializeField] private float spinCooldown;
        [SerializeField] private float jumpSpinGravity;
        
        private float _spinCd;
        private Rigidbody _rb;
        
     
        private void Start()
        {
            _rb = PlayerPhysicsScript.Rb;
            playerAnimation = GetComponent<PlayerAnimationScript>();
           
        }

        private void Update()
        {
            SpinCooldownTimer();
            SpinToAirGravity();
        }

        
        private  void SpinCooldownTimer()
        {
            if (_spinCd < 0) return;
            _spinCd -= Time.deltaTime;   
        }
        private void SpinToAirGravity()
        {
            if (playerMovement.CanSpin && !playerGroundCheck.IsGrounded)
            {
                _rb.AddForce(new Vector3(0, jumpSpinGravity, 0), ForceMode.Acceleration);
            }
        }

        private void OnFire()
        { 
            playerMovement.CanSpin  = true;
            if (_spinCd > 0) return;
            playerAnimation.SpinAnimation();
            _spinCd = spinCooldown;
        }
    }
}
