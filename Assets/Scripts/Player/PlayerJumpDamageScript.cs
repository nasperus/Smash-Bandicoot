using UnityEngine;

namespace Player
{
    public class PlayerJumpDamageScript : MonoBehaviour
    {
        [SerializeField] private Transform rayUp;
        [SerializeField] private Transform rayDown;
        [SerializeField] private float rayDistance;
        [SerializeField] private LayerMask boxMask;
        private void Update()
        {
            CheckBoxBelow();
            CheckBoxAbove();
        }
        
        private void CheckBoxBelow()
        {
            Physics.Raycast(rayDown.position, Vector3.down,  rayDistance, boxMask);

        }
        
        private void CheckBoxAbove()
        {
            Physics.Raycast(rayUp.position, Vector3.up, rayDistance, boxMask);

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
