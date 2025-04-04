using DG.Tweening;
using UnityEngine;

namespace Fruit
{
    public class FruitScript : MonoBehaviour
    {
        [Header("Spin Fruit")] [SerializeField]
        private float spinDuration;

        [SerializeField] private int spinLoops;

        [Header("Fruit go to UI")] [SerializeField]
        private Transform scoreTarget;

        [SerializeField] private float moveDuration;

        private void Start()
        {
            SpinFruit();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player")) MoveToScore();
        }

        public void SetScoreTarget(Transform target)
        {
            scoreTarget = target;
        }

        private void SpinFruit()
        {
            transform.DORotate(new Vector3(0, 360, 0), spinDuration, RotateMode.FastBeyond360)
                .SetLoops(spinLoops, LoopType.Restart)
                .SetEase(Ease.Linear);
        }

        public void MoveToScore()
        {
            if (scoreTarget == null) return;
            transform.DOMove(scoreTarget.position, moveDuration)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() => Destroy(gameObject));
        }
    }
}