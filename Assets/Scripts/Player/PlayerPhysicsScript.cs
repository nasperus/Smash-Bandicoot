using UnityEngine;

namespace Player
{
    public class PlayerPhysicsScript : MonoBehaviour
    {
    
        [SerializeField] private Rigidbody rb;
        public Rigidbody Rb => rb;
        public static PlayerPhysicsScript Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}
