using UnityEngine;

namespace Player
{
    public class PlayerPhysicsScript : MonoBehaviour
    {
    
        [SerializeField] private Rigidbody rb;
        private static PlayerPhysicsScript _instance;
        public Rigidbody Rb => rb;
        public static PlayerPhysicsScript Instance => _instance;

        private void Awake()
        {
            _instance = this;
        }
    }
}
