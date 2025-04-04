using System;
using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace Enemy
{
    public class EnemyScript : MonoBehaviour
    {
        [SerializeField] private Transform leftPoint;
        [SerializeField] private Transform rightPoint;
        [SerializeField] private float moveDuration = 2f;
        
        private static bool _isDying;
        
        private void Start()
        {
            MoveEnemy();
        }
        
        private void MoveEnemy()
        {
            transform.DOMove(rightPoint.position, moveDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() => transform.DOMove(leftPoint.position, moveDuration)
                    .SetEase(Ease.Linear)
                    .OnComplete(MoveEnemy)); 
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (_isDying) return;
                
            if (other.gameObject.TryGetComponent(out Player.PlayerMovementScript player))
            {
                Destroy(player.gameObject);
            }
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
    }
}
