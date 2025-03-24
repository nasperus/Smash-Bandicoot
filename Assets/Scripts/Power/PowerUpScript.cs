using UnityEngine;
using DG.Tweening;

namespace Power
{
    public class PowerUpScript : MonoBehaviour
    {

        [SerializeField] private Transform powerUpPosition;
        [SerializeField] private float powerMoveDuration;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                DOTween.Sequence()
                    .Append(transform.DOMove(powerUpPosition.position, powerMoveDuration))
                    .SetEase(Ease.InQuad)
                    .OnComplete(() =>
                    {
                        transform.parent = powerUpPosition;
                        transform.localPosition = Vector3.zero;
                        
                        if (GetComponent<Collider>() != null)
                            GetComponent<Collider>().enabled = false;
                    });
            }


        }
    }
}