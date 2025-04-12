using UnityEngine;

namespace Player
{
    public class PlayerDestroyBoxes : MonoBehaviour, IPlayerDoesDamage
    {
        [SerializeField] private  float sphereRadius = 1f;

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
            Gizmos.DrawWireSphere(sphereRadiusObject.position, sphereRadius);
        }

        public void DoSpinDamage()
        {
            _hitColliders = Physics.OverlapSphere(sphereRadiusObject.position, sphereRadius);

            foreach (var hitCollider in _hitColliders)
            {
                if (hitCollider.TryGetComponent(out ISpinDamageable damageable)) damageable.OnSpinDamage();
            }
        }
    }
}