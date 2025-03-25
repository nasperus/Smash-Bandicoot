using UnityEngine;
namespace Player
{
    public class PlayerDestroyBoxes : MonoBehaviour, IPlayerDoesDamage
    { 
        [Header("Sphere Radius Game Object")]
        [SerializeField] private Transform sphereRadiusObject;
        private const float SphereRadius = 1f;
        private Collider[] _hitColliders;
        public SpinDamageDelegate SpinDamageDelegate;
        

        private void OnEnable()
        {
            SpinDamageDelegate += DoSpinDamage;
        }

        private void OnDisable()
        {
            SpinDamageDelegate -= DoSpinDamage;
        }

        public void DoSpinDamage()
        {
            _hitColliders = Physics.OverlapSphere(sphereRadiusObject.position, SphereRadius);
            
            foreach (var hitCollider in _hitColliders)
            {
                if (hitCollider.TryGetComponent(out Boxes.BoxDestroyScript box))
                {
                    Destroy(box.gameObject);
                }

                if (hitCollider.TryGetComponent(out Boxes.TNTExplosiveBoxScript tnt))
                {
                    tnt.TntExplode();
                }

                if (hitCollider.TryGetComponent(out Boxes.BoxBouncePlayerScript bounce))
                {
                    Destroy(bounce.gameObject);
                }
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(sphereRadiusObject.position, SphereRadius);
        }
        
    }
}
