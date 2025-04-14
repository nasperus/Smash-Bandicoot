using Player;
using UnityEngine;

namespace Boxes
{
    public class JumpBoxScript : MonoBehaviour
    {
        [SerializeField] private float jumpForce;
        private Rigidbody _rigidBody;

        private void OnTriggerEnter (Collider other)
        {
            if (!other.gameObject.TryGetComponent(out PlayerMovementScript player)) return;
            _rigidBody = player.GetComponent<Rigidbody>();
            _rigidBody.linearVelocity = new Vector3(_rigidBody.linearVelocity.x, 0f, _rigidBody.linearVelocity.z);
            _rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}