using UnityEngine;

namespace Player
{
    public class PlayerSpinScript : MonoBehaviour
    {
        [Header("Dependencies")] [SerializeField]
        private PlayerAnimationScript playerAnimation;

        [SerializeField] private PlayerGroundCheckScript playerGroundCheck;
        [SerializeField] private PlayerMovementScript playerMovement;
        [SerializeField] private PlayerPhysicsScript playerPhysics;
        [SerializeField] private PlayerDestroyBoxes playerDestroyBoxes;


        [Header("Spin")] [SerializeField] private float spinCooldown;

        [SerializeField] private float jumpSpinGravity;
        private Rigidbody _rb;

        private float _spinCd;


        private void Start()
        {
            _rb = playerPhysics.GetRigidbody();
        }

        private void Update()
        {
            SpinCooldownTimer();
            SpinToAirGravity();
        }

        private void SpinCooldownTimer()
        {
            if (_spinCd < 0) return;
            _spinCd -= Time.deltaTime;
        }

        private void SpinToAirGravity()
        {
            if (playerMovement.CanSpin && !playerGroundCheck.IsGrounded)
                _rb.AddForce(new Vector3(0, jumpSpinGravity, 0), ForceMode.Acceleration);
        }

        private void OnFire()
        {
            playerMovement.CanSpin = true;
            if (_spinCd > 0) return;

            playerAnimation.SpinAnimation();
            _spinCd = spinCooldown;

            playerDestroyBoxes.SpinDamageDelegate?.Invoke();
        }
    }
}