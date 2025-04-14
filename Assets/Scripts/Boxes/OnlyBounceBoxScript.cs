using System.Collections;
using Player;
using UnityEngine;

namespace Boxes
{
    public class OnlyBounceBoxScript : MonoBehaviour, IPlayerBounceDamage
    {
        [SerializeField] private float jumpForce;
        
        private const float DestroyDelay = 0.1f;
        private Rigidbody _rigidBody;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out PlayerMovementScript player)) return;
            _rigidBody = player.GetComponent<Rigidbody>();
            _rigidBody.linearVelocity = new Vector3(_rigidBody.linearVelocity.x, 0f, _rigidBody.linearVelocity.z);
            _rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
        private void CheckIfDestroyOnJump()
        {
            StartCoroutine(DestroyAfterDelay(transform.parent.gameObject));
        }

        private static IEnumerator DestroyAfterDelay(GameObject box)
        {
            yield return new WaitForSeconds(DestroyDelay);
            Destroy(box);
        }

        public void PlayerBounceDamage()
        {
            CheckIfDestroyOnJump();
        }
    }
}