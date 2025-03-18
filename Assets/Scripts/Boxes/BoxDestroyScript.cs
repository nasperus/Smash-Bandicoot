using UnityEngine;
using DG.Tweening;


namespace Boxes
{
    public class BoxDestroyScript : MonoBehaviour
    {
        [Header("Instantiate Fruit")]
        [SerializeField] private GameObject fruitPrefab;

        private const float JumpHeight = 0.8f;
        private const float JumpDuration = 0.2f;
        private const float FallDuration = 0.2f;
        private Vector3 _spawnOffset;
        private Vector3 _spawnPosition;
        private GameObject _fruit;
       
        private void OnDestroy()
        {
            for (var i = 0; i < 5; i++)
            {
                _spawnOffset = new Vector3(Random.Range(-0.7f, 0.7f), 0.1f, Random.Range(-0.7f, 0.7f));
                _spawnPosition = transform.position +  _spawnOffset;
                
                _fruit = Instantiate(fruitPrefab, _spawnPosition, Quaternion.identity);
                 
                var fruitSequence = DOTween.Sequence();
                fruitSequence
                     .Append(_fruit.transform.DOMoveY(_spawnPosition.y + JumpHeight, JumpDuration));
                fruitSequence
                     .Append(_fruit.transform.DOMoveY(_spawnPosition.y, FallDuration).SetEase(Ease.InQuad));
               
            }
        }
            
        }
    
    }

