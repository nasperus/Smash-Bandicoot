using UnityEngine;

namespace Player
{
    public class PlayerGroundCheckScript : MonoBehaviour
    {
        [SerializeField] private PlayerJumpDamageScript playerJumpDamageScript;

        [Header("GroundChecks")] [SerializeField]
        private float groundDistance;

        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private GameObject groundCheck;
        private Vector3 _defaultGravity;
        public bool IsGrounded { get; private set; }

        private void Start()
        {
            if (groundCheck == null)
                groundCheck = gameObject;

            _defaultGravity = Physics.gravity;
        }

        private void Update()
        {
            DetectGround();
        }

        private void OnDrawGizmos()
        {
            if (groundCheck == null) return;
            Gizmos.color = IsGrounded ? Color.green : Color.red;
            Gizmos.DrawRay(groundCheck.transform.position, Vector3.down * groundDistance);
        }

        private void DetectGround()
        {
            IsGrounded =
                Physics.Raycast(groundCheck.transform.position, Vector3.down, groundDistance,
                    groundLayer); //|playerJumpDamageScript.BoxMask);
        }
    }
}