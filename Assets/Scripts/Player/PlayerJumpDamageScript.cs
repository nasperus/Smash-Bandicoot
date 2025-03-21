using UnityEngine;
using System.Collections;

namespace Player
{
    public class PlayerJumpDamageScript : MonoBehaviour
    {
        [SerializeField] private Transform rayUp;
        [SerializeField] private Transform rayDown;
        [SerializeField] private float rayDistance;
        [SerializeField] private LayerMask boxMask;

        private const float DestroyDelay = 0.1f;
        public LayerMask BoxMask => boxMask;

        private void Update()
        {
            CheckBoxBelow();
            CheckBoxAbove();
        }
        

        private void CheckBoxBelow()
        {
            if (!Physics.Raycast(rayDown.position, Vector3.down, out var hit, rayDistance, boxMask)) return;
            StartCoroutine(DestroyAfterDelay(hit.collider.gameObject));
            
        }
        
        private void CheckBoxAbove()
        {
            if (!Physics.Raycast(rayUp.position, Vector3.up, out var hit, rayDistance, boxMask)) return;
            StartCoroutine(DestroyAfterDelay(hit.collider.gameObject));
            
        }

        private static IEnumerator  DestroyAfterDelay(GameObject box)
        {
            yield return new WaitForSeconds(DestroyDelay);
           Destroy(box);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(rayDown.position, Vector3.down * rayDistance);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(rayUp.position, Vector3.up * rayDistance);
        }
    }
}
