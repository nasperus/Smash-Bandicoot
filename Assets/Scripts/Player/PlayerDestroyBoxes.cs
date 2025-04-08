using UnityEngine;

namespace Player
{
    public class PlayerDestroyBoxes : MonoBehaviour, IPlayerDoesDamage
    {
        private const float SphereRadius = 1f;

        [Header("Sphere Radius Game Object")] [SerializeField]
        private Transform sphereRadiusObject;

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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(sphereRadiusObject.position, SphereRadius);
        }

        public void DoSpinDamage()
        {
            _hitColliders = Physics.OverlapSphere(sphereRadiusObject.position, SphereRadius);

            foreach (var hitCollider in _hitColliders)
            {
                if (hitCollider.TryGetComponent(out ISpinDamageable damageable)) damageable.OnSpinDamage();
            }
        }
    }
}