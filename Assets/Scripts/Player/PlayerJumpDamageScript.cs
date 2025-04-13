using UnityEngine;

namespace Player
{
    public class PlayerJumpDamageScript : MonoBehaviour
    {
        [SerializeField] private Transform rayUp;
        [SerializeField] private Transform rayDown;
        [SerializeField] private float rayDistance;
        [SerializeField] private LayerMask boxMask;
        [SerializeField] private LayerMask enemyMask;


        private void Update()
        {
            CheckBoxBelow();
            CheckBoxAbove();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(rayDown.position, Vector3.down * rayDistance);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(rayUp.position, Vector3.up * rayDistance);
        }

        private void CheckBoxBelow()
        {
            if (rayDown == null) return;
            
            var hitSomething = Physics.Raycast(rayDown.position, Vector3.down, out var hit, rayDistance,
                boxMask | enemyMask);
            
            if(hitSomething && hit.collider.TryGetComponent(out IPlayerBounceDamage playerBounceDamage)) playerBounceDamage.PlayerBounceDamage();
        }

        private void CheckBoxAbove()
        {
            if (rayUp == null) return;
            var hitSomething = Physics.Raycast(rayUp.position, Vector3.up, out var hit, rayDistance, boxMask);
        }
    }
}