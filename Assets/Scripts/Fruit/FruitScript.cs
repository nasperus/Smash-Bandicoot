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

        private FruitScoreScript _scoreScript;
        
        [SerializeField] private float moveDuration;
        

        private void Start()
        {
            SpinFruit();
            
            if (_scoreScript == null)
            {
                _scoreScript = FindObjectOfType<FruitScoreScript>();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player")) 
                MoveToScore();
            
            if (_scoreScript != null)
            {
                _scoreScript.IncreaseScore();
            }
            
        }

        public void SetScore(FruitScoreScript script)
        {
            _scoreScript = script;
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