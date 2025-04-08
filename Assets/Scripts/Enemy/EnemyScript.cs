using System.Collections;
using DG.Tweening;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyScript : MonoBehaviour, ISpinDamageable
    {
        private static bool _isDying;
        [SerializeField] private Transform leftPoint;
        [SerializeField] private Transform rightPoint;
        [SerializeField] private float moveDuration = 2f;

        private void Start()
        {
            MoveEnemy();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (_isDying) return;

            if (other.gameObject.TryGetComponent(out PlayerMovementScript player)) 
                Destroy(player.gameObject);
        }

        private void MoveEnemy()
        {
            transform.DOMove(rightPoint.position, moveDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() => transform.DOMove(leftPoint.position, moveDuration)
                    .SetEase(Ease.Linear)
                    .OnComplete(MoveEnemy));
        }

        public void CheckIfDestroyOnJump()
        {
            _isDying = true;
            StartCoroutine(DestroyAfterDelay(gameObject));
        }

        private static IEnumerator DestroyAfterDelay(GameObject box)
        {
            yield return new WaitForSeconds(0.1f);
            _isDying = false;
            Destroy(box);
        }
        
        public void OnSpinDamage()
        {
            Destroy(gameObject);
        }
    }
}