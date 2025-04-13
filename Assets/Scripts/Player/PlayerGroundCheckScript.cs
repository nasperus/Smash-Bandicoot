using UnityEngine;

namespace Player
{
    public class PlayerGroundCheckScript : MonoBehaviour
    {

        [Header("GroundChecks")] [SerializeField]
        private float groundDistance;

        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private GameObject groundCheck;
        
        public bool IsGrounded { get; private set; }

        private void Start()
        {
            if (groundCheck == null)
                groundCheck = gameObject;
            
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
                    groundLayer);
        }
    }
}