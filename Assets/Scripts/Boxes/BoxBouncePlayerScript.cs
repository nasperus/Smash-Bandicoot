using System.Collections;
using Fruit;
using Player;
using UnityEngine;

namespace Boxes
{
    public class BoxBouncePlayerScript : MonoBehaviour, ISpinDamageable
    {
        private const float DestroyDelay = 0.1f;
        [SerializeField] private float jumpForce;

        [SerializeField] private GameObject fruitPrefab;
        [SerializeField] private Transform scoreTarget;
        
        private int _boxHealth = 5;
        private GameObject _fruit;

        private FruitScript _fruitScript;
        private PlayerSpinScript _playerSpinScript;
        private Rigidbody _rigidBody;


        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.TryGetComponent(out PlayerMovementScript player)) return;
            _rigidBody = player.GetComponent<Rigidbody>();

            BounceBoxForce();
            InstantiateFruitAndGetScoreReference();
            CheckBoxHealth();
            if (scoreTarget == null) return;
            _fruitScript.MoveToScore();
        }


        private void BounceBoxForce()
        {
            _rigidBody.linearVelocity = new Vector3(_rigidBody.linearVelocity.x, 0f, _rigidBody.linearVelocity.z);
            _rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        private static IEnumerator DestroyAfterDelay(GameObject box)
        {
            yield return new WaitForSeconds(DestroyDelay);
            Destroy(box);
        }

        private void InstantiateFruitAndGetScoreReference()
        {
            if (fruitPrefab == null) return;
            
            _fruit = Instantiate(fruitPrefab, transform.position, Quaternion.identity);
            _fruitScript = _fruit.GetComponent<FruitScript>();

            if (_fruitScript != null) 
                _fruitScript.SetScoreTarget(scoreTarget);
        }

        private void CheckBoxHealth()
        {
            _boxHealth--;
            if (_boxHealth <= 0) 
                StartCoroutine(DestroyAfterDelay(gameObject));
        }
        
        public void OnSpinDamage()
        {
            Destroy(gameObject);
        }
    }
    
}