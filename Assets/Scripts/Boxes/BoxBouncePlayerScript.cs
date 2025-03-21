using Player;
using UnityEngine;

namespace Boxes
{
    public class BoxBouncePlayerScript : MonoBehaviour
    {
    
        [SerializeField] private float jumpForce;
        

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.TryGetComponent(out PlayerMovementScript player)) return;
            var rigidBody = player.GetComponent<Rigidbody>();
            rigidBody.linearVelocity = new Vector3(rigidBody.linearVelocity.x, 0f, rigidBody.linearVelocity.z);
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);


        }
    }
}
