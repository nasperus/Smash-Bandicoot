using Boxes;
using UnityEngine;

namespace Player
{
    public class PlayerSpinScript : MonoBehaviour, IPlayerDoesDamage
    {
        [Header("Dependencies")]
        [SerializeField] private PlayerAnimationScript playerAnimation;
        [SerializeField ]private PlayerGroundCheckScript playerGroundCheck;
        [SerializeField] private PlayerMovementScript playerMovement;
        [SerializeField] private PlayerPhysicsScript playerPhysics;
        
        [Header("Spin")]
        [SerializeField] private float spinCooldown;
        [SerializeField] private float jumpSpinGravity;
        
        [Header("Sphere Radius Game Object")]
        [SerializeField] private Transform sphereRadiusObject;
        
        private float _spinCd;
        private Rigidbody _rb;
        
        private SpinDamageDelegate _spinDamageDelegate;
        private const float SphereRadius = 0.5f;
        private Collider[] _hitColliders;
        private bool _isTouchingBoxes;
      
     
        private void Start()
        {
            _rb = playerPhysics.GetRigidbody();
            _spinDamageDelegate = DoSpinDamage;
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
        
        public void DoSpinDamage()
        {
             _hitColliders = Physics.OverlapSphere(sphereRadiusObject.position, SphereRadius);
             _isTouchingBoxes = false;
             
            foreach (var hitCollider in _hitColliders)
            {
                if (!hitCollider.TryGetComponent(out BoxDestroyScript box)) continue;
                _isTouchingBoxes = true;
                Destroy(box.gameObject);
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = _isTouchingBoxes ? Color.green : Color.red;
            Gizmos.DrawWireSphere(sphereRadiusObject.position, SphereRadius);
        }
        
        private void OnFire()
        { 
            playerMovement.CanSpin  = true;
            if (_spinCd > 0) return;
            
            playerAnimation.SpinAnimation();
            _spinCd = spinCooldown;
            
            _spinDamageDelegate?.Invoke();
        }
        
    }
}
