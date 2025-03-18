using Boxes;
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
        private bool _isTouchingBoxes;

        private void Start()
        {
            SpinDamageDelegate = DoSpinDamage;
        }

        public void DoSpinDamage()
        {
           
            _hitColliders = Physics.OverlapSphere(sphereRadiusObject.position, SphereRadius);
            _isTouchingBoxes = false;
             
            foreach (var hitCollider in _hitColliders)
            {
                if (!hitCollider.TryGetComponent(out BoxDestroyScript box)) continue;
                _isTouchingBoxes = true;
                Destroy(box.gameObject);
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = _isTouchingBoxes ? Color.green : Color.red;
            Gizmos.DrawWireSphere(sphereRadiusObject.position, SphereRadius);
        }
   
    }
}
