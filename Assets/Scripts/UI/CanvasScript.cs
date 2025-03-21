using UnityEngine;

namespace UI
{
    public class CanvasScript : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Vector3 offset;

       private void Update()
        {
            transform.position = cameraTransform.position + offset;
        }
    }
}
