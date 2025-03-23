using System.Collections;

using UnityEngine;

namespace Boxes
{
    public class TNTExplosiveBoxScript : MonoBehaviour
    {

        [SerializeField] private GameObject explosiveEffect;
        [SerializeField] private Transform explosionStart;
        [SerializeField] private float radius;
        [SerializeField] private LayerMask explosionLayer;
        private Collider[] _radiusExplosions;

        private GameObject _effect;
        

        public void TntExplode()
        {
            if (explosionStart != null)
            {
                _radiusExplosions = Physics.OverlapSphere(explosionStart.position, radius, explosionLayer);
              
                foreach (var nearbyObjects  in _radiusExplosions)
                {
               
                    Destroy(nearbyObjects.gameObject);
                }
                _effect = Instantiate(explosiveEffect, transform.position, Quaternion.identity);
                Destroy(_effect,1f);
              
            }
            
        }

        public IEnumerator DelayExplosive()
        {
            yield return new WaitForSeconds(3);
            TntExplode();
            
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(explosionStart.position, radius);
        }
    }
}
