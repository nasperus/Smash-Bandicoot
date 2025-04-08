using System.Collections;
using Player;
using UnityEngine;

namespace Boxes
{
    public class TNTExplosiveBoxScript : MonoBehaviour, ISpinDamageable
    {
        [SerializeField] private GameObject explosiveEffect;
        [SerializeField] private Transform explosionStart;
        [SerializeField] private float radius;
        [SerializeField] private LayerMask explosionLayer;

        private GameObject _effect;
        private Collider[] _radiusExplosions;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(explosionStart.position, radius);
        }


        public void TntExplode()
        {
            if (explosionStart == null) return;
            _radiusExplosions = Physics.OverlapSphere(explosionStart.position, radius, explosionLayer);

            foreach (var nearbyObjects in _radiusExplosions) Destroy(nearbyObjects.gameObject);
            _effect = Instantiate(explosiveEffect, transform.position, Quaternion.identity);
            Destroy(_effect, 1f);
        }

        public IEnumerator DelayExplosive()
        {
            yield return new WaitForSeconds(3);
            TntExplode();
        }
        public void OnSpinDamage()
        {
            TntExplode();
        }
    }
}