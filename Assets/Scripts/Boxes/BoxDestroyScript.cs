using Player;
using UnityEngine;

namespace Boxes
{
    public class BoxDestroyScript : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {     
            other.gameObject.TryGetComponent(out PlayerDestroyBoxes playerDestroyBoxes);
            Debug.Log(other.gameObject.name);
        }
    }

      
}
