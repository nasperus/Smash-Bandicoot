using UnityEngine;

namespace Player
{
    public class PlayerPhysicsScript : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;

        public Rigidbody GetRigidbody() => rb;

    }
}