using UnityEngine;
using Player;
namespace Wall_Trap
{
    public class WallTrapDamage : MonoBehaviour
    {
        [SerializeField]private PlayerTakeDamage playerTakeDamage;
        [SerializeField] private WallTrapScript wallTrap;

        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && wallTrap.IsPlayerDamageable)
            {
                playerTakeDamage.PlayerTakeHit();
            }
        }

    }
}
