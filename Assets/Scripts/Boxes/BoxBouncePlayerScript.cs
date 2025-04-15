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
        
        [SerializeField] private GameObject brokenCratePrefab;
        [SerializeField] private Transform brokenCratePosition;
        
        private int _boxHealth = 5;
        private GameObject _fruit;

        private FruitScript _fruitScript;
        private PlayerSpinScript _playerSpinScript;
        private Rigidbody _rigidBody;
        private FruitScoreScript _fruitScoreScript;
        
        
        
        private void OnTriggerEnter(Collider other)
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
            _rigidBody.AddForce(new Vector3(0,jumpForce,0), ForceMode.Impulse);
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
            _fruitScoreScript = FindObjectOfType<FruitScoreScript>();

            if (_fruitScript != null)
            {
                _fruitScript.SetScoreTarget(scoreTarget);
                _fruitScript.SetScore(_fruitScoreScript);
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


        private void CheckBoxHealth()
        {
            _boxHealth--;
            if (_boxHealth <= 0)
            {
                DestroyCrate();
                StartCoroutine(DestroyAfterDelay(transform.parent.gameObject));
            }
               
            
        }
        
        public void OnSpinDamage()
        {
            DestroyCrate();
            Destroy(gameObject);
        }
    }
    
}