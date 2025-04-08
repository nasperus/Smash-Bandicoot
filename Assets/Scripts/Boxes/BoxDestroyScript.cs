using DG.Tweening;
using Fruit;
using Player;
using UnityEngine;

namespace Boxes
{
    public class BoxDestroyScript : MonoBehaviour, ISpinDamageable
    {
        // [Header("Player Target")]
        // [SerializeField] private Transform playerTransform;

        private const float FruitJumpHeight = 0.8f;
        private const float FruitJumpDuration = 0.2f;
        private const float FruitFallDuration = 0.2f;

        [Header("Instantiate Fruit")] [SerializeField]
        private GameObject fruitPrefab;

        [Header("Reference Fruits Prefab")] [SerializeField]
        private Transform scoreTarget;

        private GameObject _fruit;

        private FruitScript _fruitScript;

        // private const float FruitMoveToPlayerDuration =0.5f;
        private Vector3 _spawnOffset;
        private Vector3 _spawnPosition;


        private void OnDestroy()
        {
            for (var i = 0; i < 5; i++)
            {
                _spawnOffset = new Vector3(Random.Range(-0.7f, 0.7f), 0.1f, Random.Range(-0.7f, 0.7f));
                _spawnPosition = transform.position + _spawnOffset;

                _fruit = Instantiate(fruitPrefab, _spawnPosition, Quaternion.identity);
                _fruitScript = _fruit.GetComponent<FruitScript>();

                /*var fruitRigidbody = _fruit.GetComponent<Rigidbody>();

                if (fruitRigidbody != null)
                {
                    fruitRigidbody.useGravity = false;
                }*/

                if (_fruitScript != null) 
                    _fruitScript.SetScoreTarget(scoreTarget);

                var fruitSequence = DOTween.Sequence();
                fruitSequence
                    .Append(_fruit.transform.DOMoveY(_spawnPosition.y + FruitJumpHeight, FruitJumpDuration));
                fruitSequence
                    .Append(_fruit.transform.DOMoveY(_spawnPosition.y, FruitFallDuration).SetEase(Ease.InQuad));
                // fruitSequence
                //     .Append(_fruit.transform.DOMove(playerTransform.position, FruitMoveToPlayerDuration).SetEase(Ease.InOutQuad));
                //fruitSequence.SetDelay(i * 0.3f);
            }
        }

        public void OnSpinDamage()
        {
            Destroy(gameObject);
        }
    }
}