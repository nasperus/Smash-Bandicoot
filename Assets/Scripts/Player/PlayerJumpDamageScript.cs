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
        private void Update()
        {
            CheckBoxBelow();
            CheckBoxAbove();
        }
        
        private void CheckBoxBelow()
        {
            if (rayDown != null)
            {
                var hitSomething =  Physics.Raycast(rayDown.position, Vector3.down,out var hit,  rayDistance, boxMask);
                if (hitSomething && hit.collider.TryGetComponent(out Boxes.TNTExplosiveBoxScript tnt))
                {
                    Debug.Log("happend");
                    StartCoroutine(tnt.DelayExplosive());
                }
            }
           

        }
        
        private void CheckBoxAbove()
        {
            if (rayUp != null)
            {
                Physics.Raycast(rayUp.position, Vector3.up, out var hit, rayDistance, boxMask);
             
            }

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
