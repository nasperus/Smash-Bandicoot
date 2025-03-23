using UnityEngine;
using System.Collections;
using Fruit;
using Player;

namespace Boxes
{
    public class BoxBouncePlayerScript : MonoBehaviour
    {
        [SerializeField] private bool destroyImmediately;
        [SerializeField] private float jumpForce;
        
        [SerializeField] private GameObject fruitPrefab;
        [SerializeField] private Transform scoreTarget;
        
        private FruitScript _fruitScript;
        private const float DestroyDelay = 0.1f;
        private Rigidbody _rigidBody;  
        private  int _boxHealth = 5;
        private GameObject _fruit;
       

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.TryGetComponent(out PlayerMovementScript player)) return;
            _rigidBody = player.GetComponent<Rigidbody>();
            _rigidBody.linearVelocity = new Vector3(_rigidBody.linearVelocity.x, 0f, _rigidBody.linearVelocity.z);
            _rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            CheckIfDestroyOnJump();
            InstantiateFruitAndGetScoreReference();
            CheckBoxHealth();
            _fruitScript.MoveToScore();
        }

        
        private static IEnumerator DestroyAfterDelay(GameObject box)
        {
            yield return new WaitForSeconds(DestroyDelay);
            Destroy(box);
        }

        private void CheckIfDestroyOnJump()
        {
            if (!destroyImmediately) return;
            StartCoroutine(DestroyAfterDelay(gameObject));
            
        }
        private void InstantiateFruitAndGetScoreReference()
        {
            _fruit = Instantiate(fruitPrefab, transform.position, Quaternion.identity);
            _fruitScript = _fruit.GetComponent<FruitScript>();
            
            if (_fruitScript != null)
            {
                _fruitScript.SetScoreTarget(scoreTarget);
            }
        }

        private void CheckBoxHealth()
        {
            _boxHealth--;
            if (_boxHealth <= 0)
            {
                StartCoroutine(DestroyAfterDelay(gameObject));
            }
        }
        
        }
       
    }

