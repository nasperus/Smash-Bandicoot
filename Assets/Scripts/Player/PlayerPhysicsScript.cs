using UnityEngine;

namespace Player
{
    public class PlayerPhysicsScript : MonoBehaviour
    {
    
        [SerializeField] private Rigidbody rb;
        public static Rigidbody Rb => Instance.rb;
        private static PlayerPhysicsScript Instance { get; set; }

        private void Awake()
        {
            Instance = this;
            if (rb == null)
                rb = GetComponent<Rigidbody>();
        }

        public Rigidbody GetRigidbody() => rb;
    }
}
