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
        private const int Zero = 0;
        private const int X = 0;
        private const int Z = 0;
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
            if (!(_spinCd >= Zero)) return;
            _spinCd -= Time.deltaTime;   
        }
        private void SpinToAirGravity()
        {
            if (playerMovement.CanSpin && !playerGroundCheck.IsGrounded)
            {
                _rb.AddForce(new Vector3(X, jumpSpinGravity, Z), ForceMode.Acceleration);
            }
        }

        private void OnFire()
        { 
            playerMovement.CanSpin  = true;
            if (!(_spinCd <= Zero)) return;
            
            playerAnimation.SpinAnimation();
            _spinCd = spinCooldown;
        }
    }
}
