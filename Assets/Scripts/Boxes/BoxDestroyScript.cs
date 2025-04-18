using DG.Tweening;
using Fruit;
using Player;
using UnityEngine;

namespace Boxes
{
    public class BoxDestroyScript : MonoBehaviour, ISpinDamageable,IPlayerBounceDamage
    {
        [Header("Player Target")]
        [SerializeField] private Transform playerTransform;

        private const float FruitJumpHeight = 0.8f;
        private const float FruitJumpDuration = 0.2f;
        private const float FruitFallDuration = 0.2f;

        [Header("Instantiate Fruit")] [SerializeField]
        private GameObject fruitPrefab;

        [Header("Reference Fruits Prefab")] [SerializeField]
        private Transform scoreTarget;

        private GameObject _fruit;

        private FruitScript _fruitScript;
        private FruitScoreScript _fruitScoreScript;

        private const float FruitMoveToPlayerDuration =0.1f;
        [SerializeField] private GameObject brokenCratePrefab;
        [SerializeField] private Transform brokenCratePosition;
        private Vector3 _spawnOffset;
        private Vector3 _spawnPosition;


        private void OnDestroy()
        {
            for (var i = 0; i < 5; i++)
            {
                _fruitScoreScript = FindObjectOfType<FruitScoreScript>();
                _spawnOffset = new Vector3(Random.Range(-0.7f, 0.7f), 0.1f, Random.Range(-0.7f, 0.7f));
                _spawnPosition = transform.position + _spawnOffset;

                _fruit = Instantiate(fruitPrefab, _spawnPosition, Quaternion.identity);
                _fruitScript = _fruit.GetComponent<FruitScript>();
                
              
                 
                 var fruitRigidbody = _fruit.GetComponent<Rigidbody>();

                 if (fruitRigidbody != null)
                 {
                     fruitRigidbody.useGravity = false;
                 }

                if (_fruitScript != null)
                {
                    _fruitScript.SetScoreTarget(scoreTarget);
                    _fruitScript.SetScore(_fruitScoreScript);
                }
                    

                var fruitSequence = DOTween.Sequence();
                fruitSequence
                    .Append(_fruit.transform.DOMoveY(_spawnPosition.y + FruitJumpHeight, FruitJumpDuration));
                fruitSequence
                    .Append(_fruit.transform.DOMoveY(_spawnPosition.y, FruitFallDuration).SetEase(Ease.InQuad));
                 fruitSequence
                     .Append(_fruit.transform.DOMove(playerTransform.position, FruitMoveToPlayerDuration).SetEase(Ease.InOutQuad))
                     .OnUpdate(() =>
                 {
                     _fruit.transform.position = Vector3.MoveTowards(_fruit.transform.position, playerTransform.position, Time.deltaTime);
                 });
                fruitSequence.SetDelay(i * 0.1f);
            }
        }

        private void DestroyCrate()
        {
            var brokenObject = Instantiate(brokenCratePrefab, brokenCratePosition.position, Quaternion.identity);
            Rigidbody[] rigidbodies = brokenObject.GetComponentsInChildren<Rigidbody>();

            for (var i = 0; i < rigidbodies.Length; i++)
            {
                var rb = rigidbodies[i];
                
                var direction = (i < 3)
                    ? new Vector3(-1f, 1f, Random.Range(-0.5f, 0.5f))  
                    : new Vector3(1f, 1f, Random.Range(-0.5f, 0.5f));
                direction.Normalize();
                
                var force = Random.Range(5f, 6f);
                rb.AddForce(direction * force, ForceMode.Impulse);
                rb.AddTorque(Random.insideUnitSphere * 5f, ForceMode.Impulse);
                Destroy(rb.gameObject, 1f);
            }
        }

        public void PlayerBounceDamage()
        {
            DestroyCrate();
        }

        public void OnSpinDamage()
        {
            DestroyCrate();
            Destroy(gameObject);
        }
    }
}