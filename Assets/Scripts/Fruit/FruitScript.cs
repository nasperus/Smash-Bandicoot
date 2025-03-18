using Boxes;
using UnityEngine;
using DG.Tweening;


namespace  Fruit
{
    public class FruitScript : MonoBehaviour
    {
        [Header("Spin Fruit")]
        [SerializeField] private float spinDuration;
        [SerializeField] private int spinLoops;
        
        private void Start()
        {
            SpinFruit();
          
        }
        
        private void SpinFruit()
        {
            transform.DORotate(new Vector3(0,360,0),  spinDuration, RotateMode.FastBeyond360)
                .SetLoops(spinLoops, LoopType.Restart)
                .SetEase(Ease.Linear);
        }
        
    }
}

