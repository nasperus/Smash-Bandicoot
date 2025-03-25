using UnityEngine;
using System.Collections;

namespace Boxes
{
    public class OnlyBounceBoxScript : MonoBehaviour
    {
        [SerializeField] private float jumpForce;
        //[SerializeField] private bool destroyImmediately;
        private Rigidbody _rigidBody;  
        private const float DestroyDelay = 0.1f;
        
        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.TryGetComponent(out Player.PlayerMovementScript player)) return;
            _rigidBody = player.GetComponent<Rigidbody>();
            _rigidBody.linearVelocity = new Vector3(_rigidBody.linearVelocity.x, 0f, _rigidBody.linearVelocity.z);
            _rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
        
        public void CheckIfDestroyOnJump()
        {
            // if (!destroyImmediately) return;
            StartCoroutine(DestroyAfterDelay(gameObject));
            
        }
        
        private static IEnumerator DestroyAfterDelay(GameObject box)
        {
            yield return new WaitForSeconds(DestroyDelay);
            Destroy(box);
        }

    }
}
