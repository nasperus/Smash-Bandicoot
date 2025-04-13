using System;
using System.Collections;
using Player;
using TMPro;
using UnityEngine;

namespace Boxes
{
    public class TNTExplosiveBoxScript : MonoBehaviour, ISpinDamageable, IPlayerBounceDamage
    {
        [SerializeField] private GameObject explosiveEffect;
        [SerializeField] private Transform explosionStart;
        [SerializeField] private float radius;
        [SerializeField] private LayerMask explosionLayer;
        [SerializeField] private TextMeshPro textMeshPro;

        private GameObject _effect;
        private Collider[] _radiusExplosions;
        private float _countDown;
        private bool _onCountDown;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(explosionStart.position, radius);
        }

        private void Start()
        {
            textMeshPro.gameObject.SetActive(false);
            _countDown = 3;
        }

        private void Update()
        {
            CountDown();
        }

        private void CountDown()
        {
            if (!_onCountDown) return;
            _countDown -= Time.deltaTime;
            textMeshPro.text =Mathf.CeilToInt(_countDown).ToString();
        }

        public void PlayerBounceDamage()
        {
            StartCoroutine(DelayExplosive());
        }


        private void TntExplode()
        {
            if (explosionStart == null) return;
            _radiusExplosions = Physics.OverlapSphere(explosionStart.position, radius, explosionLayer);

            foreach (var nearbyObjects in _radiusExplosions) Destroy(nearbyObjects.gameObject);
            _effect = Instantiate(explosiveEffect, transform.position, Quaternion.identity);
            Destroy(_effect, 1f);
        }

        public IEnumerator DelayExplosive()
        {
            _onCountDown = true;
            textMeshPro.gameObject.SetActive(true);
            yield return new WaitForSeconds(3);
            _onCountDown = false;
            TntExplode();
        }
        public void OnSpinDamage()
        {
            TntExplode();
        }
    }
}